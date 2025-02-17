using System.Text;

namespace CrashEdit.Crash
{
    public sealed class VAB
    {
        public static VAB Join(VH vh, SampleLine[] vb)
        {
            ArgumentNullException.ThrowIfNull(vh);
            ArgumentNullException.ThrowIfNull(vb);
            if (vh.VBSize != vb.Length)
            {
                ErrorManager.SignalIgnorableError("VAB: VB size field mismatch");
            }
            SampleSet[] waves = new SampleSet[vh.Waves.Count];
            int offset = 0;
            for (int i = 0; i < vh.Waves.Count; i++)
            {
                int wavelength = vh.Waves[i];
                if (offset + wavelength > vb.Length)
                {
                    ErrorManager.SignalError("VAB: Wave ends out of bounds");
                }
                SampleLine[] wavelines = new SampleLine[wavelength];
                for (int j = 0; j < wavelength; j++)
                {
                    wavelines[j] = vb[offset + j];
                }
                offset += wavelength;
                waves[i] = new SampleSet(wavelines);
            }
            return new VAB(vh.IsOldVersion, vh.Volume, vh.Panning, vh.Attribute1, vh.Attribute2, vh.Programs, waves);
        }

        private bool isoldversion;
        private byte volume;
        private byte panning;
        private byte attribute1;
        private byte attribute2;
        private Dictionary<int, VHProgram> programs;
        private List<SampleSet> waves;

        public VAB(bool isoldversion, byte volume, byte panning, byte attribute1, byte attribute2, IDictionary<int, VHProgram> programs, IEnumerable<SampleSet> waves)
        {
            this.isoldversion = isoldversion;
            this.volume = volume;
            this.panning = panning;
            this.attribute1 = attribute1;
            this.attribute2 = attribute2;
            this.programs = new Dictionary<int, VHProgram>(programs);
            this.waves = new List<SampleSet>(waves);
        }

        public void Split(out VH vh, out SampleLine[] vb)
        {
            List<SampleLine> samples = new List<SampleLine>();
            List<int> wavelengths = new List<int>();
            foreach (SampleSet wave in waves)
            {
                samples.AddRange(wave.SampleLines);
                wavelengths.Add(wave.SampleLines.Count);
            }
            vh = new VH(isoldversion, samples.Count, volume, panning, attribute1, attribute2, programs, wavelengths);
            vb = samples.ToArray();
        }

        public byte[] Save()
        {
            Split(out VH vh, out SampleLine[] vb);
            List<byte> result = new List<byte>();
            result.AddRange(vh.Save());
            foreach (SampleLine line in vb)
            {
                result.AddRange(line.Save());
            }
            return result.ToArray();
        }

        public RIFF ToDLS()
        {
            // DLS RIFF
            RIFF dls = new RIFF("DLS ");

            // colh chunk
            int instrumentCount = programs.Count * 2;
            byte[] colh = new byte[4];
            BitConv.ToInt32(colh, 0, instrumentCount);
            dls.Items.Add(new RIFFData("colh", colh));

            // LIST lins chunk: Generates instruments from each program.  
            RIFF lins = new RIFF("lins");
            for (int i = 0; i < 128; i++)
            {
                if (programs.ContainsKey(i))
                {
                    lins.Items.Add(programs[i].ToDLSCreateIns(i, false));
                    //lins.Items.Add(programs[i].ToDLSCreateIns(i, true));
                }
            }
            dls.Items.Add(lins);

            // LIST wvpl chunk: Creates waveform data (Wave Pool).  
            RIFF wvpl = new RIFF("wvpl");
            foreach (SampleSet sampleset in waves)
            {
                List<byte> pcm = new List<byte>();
                double s0 = 0.0;
                double s1 = 0.0;
                int loopstart = 0;
                for (int i = 0; i < sampleset.SampleLines.Count; i++)
                {
                    SampleLine sampleline = sampleset.SampleLines[i];
                    pcm.AddRange(sampleline.ToPCM(ref s0, ref s1));
                    if (sampleline.Flags == SampleLineFlags.StopEnvelope)
                    {
                        break;
                    }
                    if ((sampleline.Flags & SampleLineFlags.LoopStart) != 0)
                    {
                        loopstart = i;
                    }
                }
                /*for (int i = loopstart;i < sampleset.SampleLines.Count;i++)
                {
                    SampleLine sampleline = sampleset.SampleLines[i];
                    pcm.AddRange(sampleline.ToPCM(ref s0,ref s1));
                    if ((sampleline.Flags & SampleLineFlags.LoopEnd) != 0)
                    {
                        break;
                    }
                }*/
                RIFF wave = WaveConv.ToWave(pcm.ToArray(), 44100);
                wave.Name = "wave";
                wvpl.Items.Add(wave);
            }

            // ptbl chunk: Generates offset information for each waveform.  
            int waveCount = waves.Count;
            byte[] ptbl = new byte[8 + 4 * waveCount];
            BitConv.ToInt32(ptbl, 0, 8);
            BitConv.ToInt32(ptbl, 4, waveCount);
            int waveoffset = 0;
            for (int i = 0; i < waveCount; i++)
            {
                BitConv.ToInt32(ptbl, 8 + i * 4, waveoffset);
                waveoffset += wvpl.Items[i].Length;
            }
            dls.Items.Add(new RIFFData("ptbl", ptbl));
            dls.Items.Add(wvpl);

            // LIST INFO chunk: Adds name information to the DLS file.
            RIFF info = new RIFF("INFO");
            string dlsName = "VAB Converted DLS";
            byte[] inamData = Encoding.ASCII.GetBytes(dlsName);
            info.Items.Add(new RIFFData("INAM", inamData));
            dls.Items.Add(info);

            return dls;
        }
    }
}
