namespace CrashEdit.Crash
{
    public sealed class VHTone
    {
        public static VHTone Load(byte[] data)
        {
            if (data.Length != 32)
                throw new ArgumentException("Value must be 32 bytes long.", nameof(data));
            byte priority = data[0];
            byte mode = data[1];
            byte volume = data[2];
            byte panning = data[3];
            byte centernote = data[4];
            byte pitchshift = data[5];
            byte minimumnote = data[6];
            byte maximumnote = data[7];
            byte vibratowidth = data[8];
            byte vibratotime = data[9];
            byte portamentowidth = data[10];
            byte portamentotime = data[11];
            byte pitchbendminimum = data[12];
            byte pitchbendmaximum = data[13];
            byte reserved1 = data[14];
            byte reserved2 = data[15];
            ushort adsr1 = BitConv.FromUInt16(data, 16);
            ushort adsr2 = BitConv.FromUInt16(data, 18);
            // Unused 2 bytes here
            short wave = BitConv.FromInt16(data, 22);
            short reserved3 = BitConv.FromInt16(data, 24);
            short reserved4 = BitConv.FromInt16(data, 26);
            short reserved5 = BitConv.FromInt16(data, 28);
            short reserved6 = BitConv.FromInt16(data, 30);
            if (reserved1 != 0xB1)
            {
                ErrorManager.SignalIgnorableError("VHTone: Reserved value 1 is wrong");
            }
            if (reserved2 != 0xB2)
            {
                ErrorManager.SignalIgnorableError("VHTone: Reserved value 2 is wrong");
            }
            if (reserved3 != 0xC0)
            {
                ErrorManager.SignalIgnorableError("VHTone: Reserved value 3 is wrong");
            }
            if (reserved4 != 0xC1)
            {
                ErrorManager.SignalIgnorableError("VHTone: Reserved value 4 is wrong");
            }
            if (reserved5 != 0xC2)
            {
                ErrorManager.SignalIgnorableError("VHTone: Reserved value 5 is wrong");
            }
            if (reserved6 != 0xC3)
            {
                ErrorManager.SignalIgnorableError("VHTone: Reserved value 6 is wrong");
            }
            return new VHTone(priority, mode, volume, panning, centernote, pitchshift, minimumnote, maximumnote, vibratowidth, vibratotime, portamentowidth, portamentotime, pitchbendminimum, pitchbendmaximum, adsr1, adsr2, wave);
        }

        public VHTone(bool isoldversion)
        {
            if (isoldversion)
            {
                Priority = 0;
                Mode = 0;
                Volume = 0;
                Panning = 0;
                CenterNote = 0;
                PitchShift = 0;
                MinimumNote = 0;
                MaximumNote = 0;
                VibratoWidth = 0;
                VibratoTime = 0;
                PortamentoWidth = 0;
                PortamentoTime = 0;
                PitchBendMinimum = 0;
                PitchBendMaximum = 0;
                unchecked
                {
                    ADSR1 = 0x80DF;
                    ADSR2 = 0x5FDF;
                }
            }
            else
            {
                Priority = 0;
                Mode = 0;
                Volume = 80;
                Panning = 64;
                CenterNote = 64;
                PitchShift = 0;
                MinimumNote = 64;
                MaximumNote = 64;
                VibratoWidth = 0;
                VibratoTime = 0;
                PortamentoWidth = 0;
                PortamentoTime = 0;
                PitchBendMinimum = 0;
                PitchBendMaximum = 0;
                unchecked
                {
                    ADSR1 = 0x80DF;
                    ADSR2 = 0x5FDF;
                }
            }
            Wave = 0;
        }

        // This is ridiculous! There has to be a better way.
        public VHTone(byte priority, byte mode, byte volume, byte panning, byte centernote, byte pitchshift, byte minimumnote, byte maximumnote, byte vibratowidth, byte vibratotime, byte portamentowidth, byte portamentotime, byte pitchbendminimum, byte pitchbendmaximum, ushort adsr1, ushort adsr2, short wave)
        {
            Priority = priority;
            Mode = mode;
            Volume = volume;
            Panning = panning;
            CenterNote = centernote;
            PitchShift = pitchshift;
            MinimumNote = minimumnote;
            MaximumNote = maximumnote;
            VibratoWidth = vibratowidth;
            VibratoTime = vibratotime;
            PortamentoWidth = portamentowidth;
            PortamentoTime = portamentotime;
            PitchBendMinimum = pitchbendminimum;
            PitchBendMaximum = pitchbendmaximum;
            ADSR1 = adsr1;
            ADSR2 = adsr2;
            Wave = wave;
        }

        public byte Priority { get; }
        public byte Mode { get; }
        public byte Volume { get; }
        public byte Panning { get; }
        public byte CenterNote { get; }
        public byte PitchShift { get; }
        public byte MinimumNote { get; }
        public byte MaximumNote { get; }
        public byte VibratoWidth { get; }
        public byte VibratoTime { get; }
        public byte PortamentoWidth { get; }
        public byte PortamentoTime { get; }
        public byte PitchBendMinimum { get; }
        public byte PitchBendMaximum { get; }
        public ushort ADSR1 { get; }
        public ushort ADSR2 { get; }
        public short Wave { get; }

        public byte[] Save(int program)
        {
            byte[] data = new byte[32];
            data[0] = Priority;
            data[1] = Mode;
            data[2] = Volume;
            data[3] = Panning;
            data[4] = CenterNote;
            data[5] = PitchShift;
            data[6] = MinimumNote;
            data[7] = MaximumNote;
            data[8] = VibratoWidth;
            data[9] = VibratoTime;
            data[10] = PortamentoWidth;
            data[11] = PortamentoTime;
            data[12] = PitchBendMinimum;
            data[13] = PitchBendMaximum;
            data[14] = 0xB1;
            data[15] = 0xB2;
            BitConv.ToUInt16(data, 16, ADSR1);
            BitConv.ToUInt16(data, 18, ADSR2);
            BitConv.ToInt16(data, 20, (short)program);
            BitConv.ToInt16(data, 22, Wave);
            BitConv.ToInt16(data, 24, 0xC0);
            BitConv.ToInt16(data, 26, 0xC1);
            BitConv.ToInt16(data, 28, 0xC2);
            BitConv.ToInt16(data, 30, 0xC3);
            return data;
        }

        // values from VGMTrans
        // https://github.com/vgmtrans/vgmtrans/blob/master/src/main/conversion/DLSFile.h
        const short CONN_DST_PAN = 0x0004;
        const short CONN_DST_EG1_ATTACKTIME = 0x0206;
        const short CONN_DST_EG1_HOLDTIME = 0x020c;
        const short CONN_DST_EG1_DECAYTIME = 0x0207;
        const short CONN_DST_EG1_SUSTAINLEVEL = 0x020a;
        const short CONN_DST_EG1_RELEASETIME = 0x0209;

        const int DLS_DECIBEL_UNIT = 65536; // DLS1 spec p25

        public RIFF ToDLSCreatergn2(VAB vab, VHProgram prog, bool drumkit)
        {
            RIFF rgn = new RIFF("rgn2");

            int sampleID = (Wave > 0) ? Wave - 1 : 0;

            SampleSet sampleset = vab.Waves[sampleID];
            bool loopStatus = false;
            int ulLoopType = 0;
            int ulLoopStart = 0;
            int ulLoopLength = 0;
            if (sampleset.LoopStart > 0)
            {
                loopStatus = true;
                ulLoopStart = sampleset.LoopStart / 2;
                ulLoopLength = (sampleset.LoopEnd - sampleset.LoopStart) / 2;
            }

            // We'll use the default range.
            short VelLow = 0;
            short VelHigh = 127;

            // rgnh
            byte[] rgnh = new byte[14];
            BitConv.ToInt16(rgnh, 0, MinimumNote);          // usKeyLow
            BitConv.ToInt16(rgnh, 2, MaximumNote);          // usKeyHigh
            BitConv.ToInt16(rgnh, 4, VelLow);               // usVelLow
            BitConv.ToInt16(rgnh, 6, VelHigh);              // usVelHigh
            BitConv.ToInt16(rgnh, 8, 1);                    // fusOptions
            BitConv.ToInt16(rgnh, 10, 0);                   // usKeyGroup
            BitConv.ToInt16(rgnh, 12, 1);                   // new for DLS2
            rgn.Items.Add(new RIFFData("rgnh", rgnh));

            // wsmp
            byte[] wsmp = loopStatus ? new byte[36] : new byte[20];
            BitConv.ToInt32(wsmp, 0, 20);                   // cbSize (size of structure without loop record)
                                                            // Unity Note
            BitConv.ToInt16(wsmp, 4, (byte)(CenterNote - (PitchShift / 100)));
                                                            // Fine Tune
            BitConv.ToInt16(wsmp, 6, (byte)(SF2Conv.CalcFineTune(PitchShift) % 100));
                                                            // lAttenuation
            BitConv.ToInt32(wsmp, 8, SF2Conv.DLSConvertVolumeToInitialAttenuation(prog.Volume, Volume));
            BitConv.ToInt32(wsmp, 12, 1);                   // fulOptions
            if (loopStatus)
            {
                BitConv.ToInt32(wsmp, 16, 1);               // cSampleLoops
                BitConv.ToInt32(wsmp, 20, 16);
                BitConv.ToInt32(wsmp, 24, ulLoopType);      // ulLoopType
                BitConv.ToInt32(wsmp, 28, ulLoopStart);     // ulLoopStart
                BitConv.ToInt32(wsmp, 32, ulLoopLength);    // ulLoopLength
            }
            else
            {
                BitConv.ToInt32(wsmp, 16, 0);               // cSampleLoops: no loop
            }
            rgn.Items.Add(new RIFFData("wsmp", wsmp));

            // wlnk
            byte[] wlnk = new byte[12];
            BitConv.ToInt16(wlnk, 0, 0);                    // fusOptions
            BitConv.ToInt16(wlnk, 2, 0);                    // usPhaseGroup
            int channel = drumkit ? 3 : 1;
            BitConv.ToInt32(wlnk, 4, channel);              // ulChannel
            int tableIndex = sampleID;
            BitConv.ToInt32(wlnk, 8, tableIndex);           // ulTableIndex
            rgn.Items.Add(new RIFFData("wlnk", wlnk));

            // lar2
            RIFF lar2 = Createlar2Chunk();
            rgn.Items.Add(lar2);

            return rgn;
        }

        public RIFF Createlar2Chunk()
        {
            RIFF lar2 = new RIFF("lar2");

            byte[] art2Data = new byte[68];

            BitConv.ToInt32(art2Data, 0, 8); // cbSize: set 8
            BitConv.ToInt32(art2Data, 4, 5); // cCues: set the number of connection blocks (5 blocks for Pan + ADSR in this case)

            ADSR envelope = PSXADSR.ComputeADSR(ADSR1, ADSR2);

            // Calculate ADSR for DLS.
            long convAttack = (long)Math.Round(SF2Conv.SecondsToTimecents(envelope.AttackTime) * 65536);
            long convDecay = (long)Math.Round(SF2Conv.SecondsToTimecents(envelope.DecayTime) * 65536);
            long convSustainLev;
            if (envelope.SustainLevel == -1)
                convSustainLev = 0x03e80000; // sustain at full if no sustain level provided
            else
            {
                // The DLS envelope is a range from 0 to -96db.
                double attenInDB = SF2Conv.ConvertLogScaleValToAtten(envelope.SustainLevel);
                convSustainLev = (long)(((96.0 - attenInDB) / 96.0) * 0x03e80000);
            }
            long convRelease = (long)Math.Round(SF2Conv.SecondsToTimecents(envelope.ReleaseTime) * 65536);

            int offset = 8;

            // block 1: Pan
            BitConv.ToInt16(art2Data, offset, 0);
            BitConv.ToInt16(art2Data, offset + 2, 0);
            BitConv.ToInt16(art2Data, offset + 4, CONN_DST_PAN);
            BitConv.ToInt16(art2Data, offset + 6, 0);
            BitConv.ToInt16(art2Data, offset + 8, 0);
            BitConv.ToInt16(art2Data, offset + 10, SF2Conv.ConvertPanByte(Panning));
            offset += 12;

            // block 2: Attack
            BitConv.ToInt16(art2Data, offset, 0);
            BitConv.ToInt16(art2Data, offset + 2, 0);
            BitConv.ToInt16(art2Data, offset + 4, CONN_DST_EG1_ATTACKTIME);
            BitConv.ToInt16(art2Data, offset + 6, 0);
            BitConv.ToInt32(art2Data, offset + 8, Convert.ToInt32(convAttack));
            offset += 12;

            //// block 2: Hold
            //BitConv.ToInt16(art2Data, offset, 0);
            //BitConv.ToInt16(art2Data, offset + 2, 0);
            //BitConv.ToInt16(art2Data, offset + 4, CONN_DST_EG1_HOLDTIME);
            //BitConv.ToInt16(art2Data, offset + 6, 0);
            //BitConv.ToInt32(art2Data, offset + 8, Convert.ToInt32(convHoldTime));
            //offset += 12;

            // block 3: Decay
            BitConv.ToInt16(art2Data, offset, 0);
            BitConv.ToInt16(art2Data, offset + 2, 0);
            BitConv.ToInt16(art2Data, offset + 4, CONN_DST_EG1_DECAYTIME);
            BitConv.ToInt16(art2Data, offset + 6, 0);
            BitConv.ToInt32(art2Data, offset + 8, Convert.ToInt32(convDecay));
            offset += 12;

            // block 4: Sustain
            BitConv.ToInt16(art2Data, offset, 0);
            BitConv.ToInt16(art2Data, offset + 2, 0);
            BitConv.ToInt16(art2Data, offset + 4, CONN_DST_EG1_SUSTAINLEVEL);
            BitConv.ToInt16(art2Data, offset + 6, 0);
            BitConv.ToInt32(art2Data, offset + 8, Convert.ToInt32(convSustainLev));
            offset += 12;

            // block 5: Release
            BitConv.ToInt16(art2Data, offset, 0);
            BitConv.ToInt16(art2Data, offset + 2, 0);
            BitConv.ToInt16(art2Data, offset + 4, CONN_DST_EG1_RELEASETIME);
            BitConv.ToInt16(art2Data, offset + 6, 0);
            BitConv.ToInt32(art2Data, offset + 8, Convert.ToInt32(convRelease));
            offset += 12;

            lar2.Items.Add(new RIFFData("art2", art2Data));
            return lar2;
        }


    }
}
