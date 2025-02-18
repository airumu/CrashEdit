using System.Reflection;
using System.Text;
using CrashEdit.Crash;

public static class SF2Conv
{
    // values from VGMTrans
    // https://github.com/vgmtrans/vgmtrans/blob/master/src/main/conversion/SF2File.h
    public enum Op : ushort
    {
        // Oscillator
        startAddrsOffset,       //sample start address -4 (0 to 0xffffff)   0
        endAddrsOffset,
        startloopAddrsOffset,   //loop start address -4 (0 to 0xffffff)
        endloopAddrsOffset,     //loop end address -3 (0 to 0xffffff)

        // Pitch
        startAddrsCoarseOffset, //CHANGED FOR SF2
        modLfoToPitch,          //main fm: lfo1-> pitch                     5
        vibLfoToPitch,          //aux fm:  lfo2-> pitch
        modEnvToPitch,          //pitch env: env1(aux)-> pitch

        // Filter
        initialFilterFc,        //initial filter cutoff
        initialFilterQ,         //filter Q
        modLfoToFilterFc,       //filter modulation: lfo1 -> filter cutoff  10
        modEnvToFilterFc,       //filter env: env1(aux)-> filter cutoff

        // Amplifier
        endAddrsCoarseOffset,   //CHANGED FOR SF2
        modLfoToVolume,         //tremolo: lfo1-> volume
        unused1,

        // Effects
        chorusEffectsSend,      //chorus                                    15
        reverbEffectsSend,      //reverb
        pan,
        unused2,
        unused3,
        unused4,                //                                          20

        // Main lfo1
        delayModLFO,            //delay 0x8000-n*(725us)
        freqModLFO,             //frequency

        // Aux lfo2
        delayVibLFO,            //delay 0x8000-n*(725us)
        freqVibLFO,             //frequency

        // Env1(aux/value)
        delayModEnv,            //delay 0x8000 - n(725us)                   25
        attackModEnv,           //attack
        holdModEnv,             //hold
        decayModEnv,            //decay
        sustainModEnv,          //sustain
        releaseModEnv,          //release                                   30
        keynumToModEnvHold,
        keynumToModEnvDecay,

        // Env2(ampl/vol)
        delayVolEnv,            //delay 0x8000 - n(725us)
        attackVolEnv,           //attack
        holdVolEnv,             //hold                                      35
        decayVolEnv,            //decay
        sustainVolEnv,          //sustain
        releaseVolEnv,          //release
        keynumToVolEnvHold,
        keynumToVolEnvDecay,    //                                          40

        // Preset
        instrument,
        reserved1,
        keyRange,
        velRange,
        startloopAddrCoarseOffset, //CHANGED FOR SF2                       45
        keynum,
        velocity,
        initialAttenuation,            //CHANGED FOR SF2
        reserved2,
        endloopAddrsCoarseOffset,   //CHANGED FOR SF2                       50
        coarseTune,
        fineTune,
        sampleID,
        sampleModes,                //CHANGED FOR SF2
        reserved3,                  //                                      55
        scaleTuning,
        exclusiveClass,
        overridingRootKey,
        unused5,
        endOper                     //                                      60
    }

    private static bool debug = false;
    private static void DebugOutput(string str)
    {
        if (debug) Console.WriteLine(str);
    }

    private static List<SampleSet> vhWaves = new();
    private static List<byte> sdtaData = new();
    private static List<int> sampleLengths = new();
    private static List<VHTone> vhTones = new();

    public static byte[] ToSF2(VAB vab, bool debug)
    {
        SF2Conv.debug = debug;
        DebugOutput("Start converting...");

        vhWaves = new List<SampleSet>();
        sdtaData = new List<byte>();
        sampleLengths = new List<int>();
        vhTones = new List<VHTone>();

        vab.Split(out VH vh, out SampleLine[] allSampleLines);

        // Generate PCM from SampleSet.
        foreach (SampleSet wave in GetWaves(vab))
        {
            List<byte> waveData = new List<byte>();
            double s0 = 0.0, s1 = 0.0;
            foreach (SampleLine sample in wave.SampleLines)
            {
                if (sample.Flags == SampleLineFlags.LoopStart || sample.Flags == SampleLineFlags.LoopStartAlt)
                {
                    wave.LoopStart = waveData.Count;
                }

                byte[] pcm = sample.ToPCM(ref s0, ref s1);
                waveData.AddRange(pcm);

                if (sample.Flags == SampleLineFlags.StopEnvelope)
                {
                    wave.LoopStart = 0;
                    wave.LoopEnd = waveData.Count;
                    break;
                }
                if (sample.Flags == SampleLineFlags.LoopEnd)
                {
                    wave.LoopEnd = waveData.Count;
                    break;
                }
            }
            sampleLengths.Add(waveData.Count);
            sdtaData.AddRange(waveData);
            vhWaves.Add(wave);
        }

        // Get VHTones.
        foreach (var program in vh.Programs)
        {
            VHProgram vhProgram = program.Value;
            for (int i = 0; i < vhProgram.Tones.Count; i++)
            {
                VHTone tone = vhProgram.Tones[i];
                vhTones.Add(tone);
            }
        }

        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            WriteASCII(bw, "RIFF");
            bw.Write(0); // filesize placeholder
            WriteASCII(bw, "sfbk");

            WriteINFOChunk(bw);
            WriteSdtaChunk(bw, sdtaData.ToArray());
            WritePdtaChunk(bw, vh, 44100);

            int fileSize = (int)ms.Length - 8;
            ms.Position = 4;
            bw.Write(fileSize);

            return ms.ToArray();
        }
    }

    // Get "waves" from vh with Reflection.
    private static IEnumerable<SampleSet> GetWaves(VAB vab)
    {
        FieldInfo? field = typeof(VAB).GetField("waves", BindingFlags.NonPublic | BindingFlags.Instance);
        if (field == null)
        {
            throw new InvalidOperationException("Field 'waves' not found in VAB type.");
        }
        var waves = field.GetValue(vab) as List<SampleSet>;
        if (waves == null)
        {
            throw new InvalidOperationException("Field 'waves' is null.");
        }
        return waves;
    }

    // Get VH.Programs, create and sort the list.
    private static List<KeyValuePair<int, VHProgram>> GetSortedPrograms(VH vh)
    {
        List<KeyValuePair<int, VHProgram>> list = new(vh.Programs);
        list.Sort((a, b) => a.Key.CompareTo(b.Key));
        return list;
    }

    // Calculates the cumulative offset of the zones for each instrument (preset).  
    // If an instrument has Tones, the number of zones is determined by the number of Tones; otherwise, it is set to 1.
    private static List<int> ComputeInstZoneOffsets(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        List<int> offsets = new List<int>();
        int offs = 0;
        foreach (var prog in programs)
        {
            offsets.Add(offs);
            int zones = (prog.Value.Tones.Count > 0) ? prog.Value.Tones.Count : 1;
            offs += zones;
        }
        return offsets;
    }

    #region Helpers

    private static void WriteASCII(BinaryWriter bw, string s)
    {
        bw.Write(Encoding.ASCII.GetBytes(s));
    }

    private static void WriteListChunk(MemoryStream ms, BinaryWriter bw)
    {
        byte[] listData = ms.ToArray();
        WriteChunk(bw, "LIST", listData);
    }

    private static void WriteChunk(BinaryWriter bw, string id, byte[] data)
    {
        WriteASCII(bw, id);
        bw.Write(data.Length % 2 == 0 ? data.Length : data.Length + 1);
        bw.Write(data);
        if (data.Length % 2 != 0)
            bw.Write((byte)0);
    }

    private static void WriteStringSubChunk(BinaryWriter bw, string id, string value)
    {
        byte[] data = Encoding.ASCII.GetBytes(value);
        WriteChunk(bw, id, data);
    }

    private static void WriteStringSubChunk(BinaryWriter bw, string id, byte[] data)
    {
        WriteChunk(bw, id, data);
    }

    #endregion

    #region INFO chunk

    private static void WriteINFOChunk(BinaryWriter bw)
    {
        using (MemoryStream msSub = new MemoryStream())
        using (BinaryWriter bwSub = new BinaryWriter(msSub))
        {
            WriteStringSubChunk(bwSub, "ifil", new byte[] { 2, 0, 1, 0 });
            WriteStringSubChunk(bwSub, "isng", "EMU8000");
            WriteStringSubChunk(bwSub, "INAM", "SynthFile");
            WriteStringSubChunk(bwSub, "ICRD", DateTime.Now.ToString("yyyy-MM-dd"));
            WriteStringSubChunk(bwSub, "ISFT", $"CrashEdit v0.4.0.1");
            byte[] infoData = msSub.ToArray();

            using (MemoryStream msList = new MemoryStream())
            using (BinaryWriter bwList = new BinaryWriter(msList))
            {
                WriteASCII(bwList, "INFO");
                bwList.Write(infoData);
                WriteListChunk(msList, bw);
            }
        }
    }

    #endregion

    #region sdta chunk

    private static void WriteSdtaChunk(BinaryWriter bw, byte[] sampleData)
    {
        const int sf2Padding = 46 * 2; // plus the 46 padding samples required by sf2 spec

        using (MemoryStream msList = new MemoryStream())
        using (BinaryWriter bwList = new BinaryWriter(msList))
        {
            WriteASCII(bwList, "sdta");
            WriteASCII(bwList, "smpl");
            bwList.Write(sampleData.Length + sf2Padding);
            byte[] paddedSampleData = new byte[sampleData.Length + sf2Padding];
            Array.Copy(sampleData, paddedSampleData, sampleData.Length);
            bwList.Write(paddedSampleData);

            WriteListChunk(msList, bw);
        }
    }

    #endregion

    #region pdta chunk

    private static void WritePdtaChunk(BinaryWriter bw, VH vh, int sampleRate)
    {
        using (MemoryStream msList = new MemoryStream())
        using (BinaryWriter bwList = new BinaryWriter(msList))
        {
            WriteASCII(bwList, "pdta");

            // Preset Data
            byte[] phdr = CreatePhdrChunk(vh);
            WriteChunk(bwList, "phdr", phdr);
            byte[] pbag = CreatePbagChunk(vh);
            WriteChunk(bwList, "pbag", pbag);
            byte[] pmod = CreatePmodChunk();
            WriteChunk(bwList, "pmod", pmod);
            byte[] pgen = CreatePgenChunk(vh);
            WriteChunk(bwList, "pgen", pgen);

            // Instrument Data
            byte[] inst = CreateInstChunk(vh);
            WriteChunk(bwList, "inst", inst);
            byte[] ibag = CreateIbagChunk(vh);
            WriteChunk(bwList, "ibag", ibag);
            byte[] imod = CreateImodChunk();
            WriteChunk(bwList, "imod", imod);
            byte[] igen = CreateIgenChunk(vh);
            WriteChunk(bwList, "igen", igen);

            // Sample Data
            byte[] shdr = CreateShdrChunk(sampleRate);
            WriteChunk(bwList, "shdr", shdr);

            WriteListChunk(msList, bw);
        }
    }

    /// <summary>
    /// PDTA - phdr: Outputs the preset headers for each preset.
    /// For each preset, sets wPresetBagNdx to the starting index of the corresponding preset bag (i.e., the index of that instrument).
    /// </summary>
    private static byte[] CreatePhdrChunk(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        int presetCount = programs.Count;
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            for (int i = 0; i < presetCount; i++)
            {
                byte[] entry = new byte[38];
                string presetName = "Preset " + programs[i].Key;
                byte[] nameBytes = Encoding.ASCII.GetBytes(presetName);
                Array.Copy(nameBytes, 0, entry, 0, Math.Min(20, nameBytes.Length)); // achPresetName
                BitConverter.GetBytes((ushort)programs[i].Key).CopyTo(entry, 20); // wPreset
                BitConverter.GetBytes((ushort)0).CopyTo(entry, 22); // wBank: we'll use 0 for now
                BitConverter.GetBytes((ushort)i).CopyTo(entry, 24); // wPresetBagNdx
                BitConverter.GetBytes(0).CopyTo(entry, 26); // dwLibrary
                BitConverter.GetBytes(0).CopyTo(entry, 30); // dwGenre
                BitConverter.GetBytes(0).CopyTo(entry, 34); // dwMorphology
                bw.Write(entry);
                DebugOutput($"phdr [{i:D2}]: Key {programs[i].Key:D2}, wPreset {i}");
            }
            // add terminal
            byte[] terminal = new byte[38];
            BitConverter.GetBytes((ushort)presetCount).CopyTo(terminal, 24); // wPresetBagNdx
            bw.Write(terminal);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// PDTA - pbag: Outputs the bag entries for each preset.
    /// The wGenNdx of each entry is set to the starting index of the preset's pgen, which is the same as the preset number.
    /// </summary>
    private static byte[] CreatePbagChunk(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        int presetCount = programs.Count;
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            for (int i = 0; i < presetCount; i++)
            {
                bw.Write((ushort)i); // wGenNdx = i
                bw.Write((ushort)0); // wModNdx = 0
                DebugOutput($"pbag [{i:D2}]: wGenNdx {i}");
            }
            // add terminal: wGenNdx = presetCount
            bw.Write((ushort)presetCount);
            bw.Write((ushort)0);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// PDTA - pmod: Just create the terminal field.
    /// </summary>
    private static byte[] CreatePmodChunk()
    {
        return new byte[10];
    }

    /// <summary>
    /// PDTA - pgen: Outputs the generator entries for each preset.
    /// </summary>
    private static byte[] CreatePgenChunk(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        int presetCount = programs.Count;
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            for (int i = 0; i < presetCount; i++)
            {
                bw.Write((ushort)Op.instrument); // sfGenOper
                bw.Write((short)i);  // wAmount
                DebugOutput($"pgen [{i:D2}]: wAmount {i}");
            }
            // add terminal
            bw.Write(new byte[4]);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// PDTA - inst: Outputs the instrument headers.
    /// Each preset corresponds to a single instrument.
    /// Sets wInstBagNdx to set the starting index of the zones for each instrument.
    /// </summary>
    private static byte[] CreateInstChunk(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        var instZoneOffsets = ComputeInstZoneOffsets(vh);
        int presetCount = programs.Count;
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            for (int i = 0; i < presetCount; i++)
            {
                byte[] entry = new byte[22];
                string instName = "Inst " + programs[i].Key;
                byte[] nameBytes = Encoding.ASCII.GetBytes(instName);
                Array.Copy(nameBytes, 0, entry, 0, Math.Min(20, nameBytes.Length)); // achInstName
                BitConverter.GetBytes((ushort)instZoneOffsets[i]).CopyTo(entry, 20); // wInstBagNdx
                bw.Write(entry);
                DebugOutput($"inst [{i:D2}]: Key {programs[i].Key:D2}, wInstBagNdx {instZoneOffsets[i]}");
            }
            // add terminal
            // terminalOffset = Start offset of the last instrument + Number of zones for that instrument.
            int lastZones = (programs[presetCount - 1].Value.Tones.Count > 0)
                ? programs[presetCount - 1].Value.Tones.Count
                : 1;
            ushort terminalOffset = (ushort)(instZoneOffsets[presetCount - 1] + lastZones);
            byte[] terminal = new byte[22];
            BitConverter.GetBytes(terminalOffset).CopyTo(terminal, 20);
            bw.Write(terminal);
            DebugOutput($"  terminal_wInstBagNdx: {terminalOffset}");
            return ms.ToArray();
        }
    }

    /// <summary>
    /// PDTA - ibag: Outputs the instrument bag entries.
    /// </summary>
    private static byte[] CreateIbagChunk(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        int presetCount = programs.Count;
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            int rgnCounter = 0;
            int instGenCounter = 0;
            for (int i = 0; i < presetCount; i++)
            {
                VHProgram prog = programs[i].Value;
                int zoneCount = (prog.Tones.Count > 0) ? prog.Tones.Count : 1;
                for (int j = 0; j < zoneCount; j++)
                {
                    bw.Write((ushort)instGenCounter); // wInstGenNdx: set the cumulative counter
                    bw.Write((ushort)0); // wInstModNdx
                    DebugOutput($"ibag [{i:D2}]: wInstGenNdx {instGenCounter}");
                    instGenCounter += 12; // C++ では 12 バイトずつ加算
                    rgnCounter++;
                }
            }
            // add terminal
            bw.Write((ushort)instGenCounter);
            bw.Write((ushort)0);
            DebugOutput($"  terminal_wInstGenNdx: {instGenCounter}");
            return ms.ToArray();
        }
    }

    /// <summary>
    /// PDTA - imod: Just create the terminal field.
    /// </summary>
    private static byte[] CreateImodChunk()
    {
        return new byte[10];
    }

    /// <summary>  
    /// PDTA - igen: Outputs the generator entries for each instrument.
    /// Each instrument contains multiple zones (Tones).
    /// </summary>
    private static byte[] CreateIgenChunk(VH vh)
    {
        var programs = GetSortedPrograms(vh);
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            for (int i = 0; i < programs.Count; i++)
            {
                VHProgram prog = programs[i].Value;
                DebugOutput($"igen: VHProgram[{i}], VHTone count {prog.Tones.Count}");
                foreach (VHTone tone in prog.Tones)
                {
                    ADSR envelope = PSXADSR.ComputeADSR(tone.ADSR1, tone.ADSR2);
                    short sampleID = (short)(tone.Wave > 0 ? tone.Wave - 1 : 0);

                    // keyRange operator
                    bw.Write((ushort)Op.keyRange);
                    short keyRange = (short)((tone.MinimumNote & 0xFF) | ((tone.MaximumNote & 0xFF) << 8));
                    bw.Write(keyRange);

                    // velocity range
                    // We'll use the default range.
                    bw.Write((ushort)Op.velRange);
                    short byLo = 0;
                    short byHi = 127;
                    short velRange = (short)((byLo & 0xFF) | ((byHi & 0xFF) << 8));
                    bw.Write(velRange);

                    // initialAttenuation
                    bw.Write((ushort)Op.initialAttenuation);
                    bw.Write((short)ConvertVolumeToInitialAttenuation(tone.Volume));

                    // pan
                    bw.Write((ushort)Op.pan);
                    bw.Write((short)ConvertPanByte(tone.Panning));

                    // sampleModes
                    SampleSet sampleset = vhWaves[sampleID];
                    if (sampleset.LoopStart > 0)
                    {
                        bw.Write((ushort)Op.sampleModes);
                        bw.Write((short)1);
                    }
                    else
                    {
                        bw.Write((ushort)Op.sampleModes);
                        bw.Write((short)0);
                    }

                    // overridingRootKey
                    bw.Write((ushort)Op.overridingRootKey);
                    bw.Write((short)tone.CenterNote);

                    // attackVolEnv
                    bw.Write((ushort)Op.attackVolEnv);
                    bw.Write((short)(envelope.AttackTime == 0 ? -32768 : Math.Round(SecondsToTimecents(envelope.AttackTime))));

                    // holdVolEnv
                    bw.Write((ushort)Op.holdVolEnv);
                    bw.Write((short)(envelope.HoldTime == 0 ? -32768 : Math.Round(SecondsToTimecents(envelope.HoldTime))));

                    // decayVolEnv
                    bw.Write((ushort)Op.decayVolEnv);
                    bw.Write((short)(envelope.DecayTime == 0 ? -32768 : Math.Round(SecondsToTimecents(envelope.DecayTime))));

                    // sustainVolEnv
                    bw.Write((ushort)Op.sustainVolEnv);
                    if (envelope.SustainLevel > 100.0)
                        envelope.SustainLevel = 100.0;
                    bw.Write((short)envelope.SustainLevel);

                    // releaseVolEnv
                    bw.Write((ushort)Op.releaseVolEnv);
                    bw.Write((short)(envelope.ReleaseTime == 0 ? -32768 : Math.Round(SecondsToTimecents(envelope.ReleaseTime))));

                    // sampleID
                    bw.Write((ushort)Op.sampleID);
                    bw.Write((short)sampleID);
                    DebugOutput($"  sampleID: {sampleID}, KeyRange: {tone.MinimumNote} - {tone.MaximumNote}");
                }
            }
            // add terminal
            bw.Write(new byte[4]);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// PDTA - shdr: Outputs the sample headers.
    /// Each header is 46 bytes long.
    /// dwStart, dwEnd, dwStartloop and dwEndloop are specified in PCM 16-bit units (byte size ÷ 2).
    /// </summary>
    private static byte[] CreateShdrChunk(int sampleRate)
    {
        DebugOutput("==== shdr ====");
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            int sampleOffset = 0;
            int sampleIndex = 0;
            foreach (int length in sampleLengths)
            {
                SampleSet sampleset = vhWaves[sampleIndex];
                // Find the VHTone we need in vhTones.
                VHTone? tone = null;
                foreach (VHTone _tone in vhTones)
                {
                    if (_tone.Wave - 1 == sampleIndex)
                    {
                        tone = _tone;
                        break;
                    }
                }
                byte[] entry = new byte[46];
                string sampleName = "Sample " + sampleIndex;
                byte[] nameBytes = Encoding.ASCII.GetBytes(sampleName);
                Array.Copy(nameBytes, 0, entry, 0, Math.Min(20, nameBytes.Length)); // achSampleName
                int dwStart = sampleOffset / 2;
                BitConverter.GetBytes(dwStart).CopyTo(entry, 20);                   // dwStart
                int dwEnd = dwStart + (length / 2);
                BitConverter.GetBytes(dwEnd).CopyTo(entry, 24);                     // dwEnd
                int dwStartloop = 0;
                int dwEndloop = 0;
                if (sampleset.LoopStart > 0)
                {
                    dwStartloop = dwStart + (sampleset.LoopStart / 2);
                    dwEndloop = dwStart + (sampleset.LoopEnd / 2);
                    DebugOutput($"Sample[{sampleIndex:D2}]: offset: {sampleOffset:D7}, length: {length:D6}, LoopStart: {dwStartloop * 2:D7}, LoopEnd: {dwEndloop * 2:D7}");
                }
                else
                {
                    DebugOutput($"Sample[{sampleIndex:D2}]: offset: {sampleOffset:D7}, length: {length:D6}");
                }
                BitConverter.GetBytes(dwStartloop).CopyTo(entry, 28);               // dwStartloop
                BitConverter.GetBytes(dwEndloop).CopyTo(entry, 32);                 // dwEndloop
                BitConverter.GetBytes(sampleRate).CopyTo(entry, 36);                // dwSampleRate
                if (tone != null)
                {
                    byte byOriginalKey = (byte)(tone.CenterNote - (tone.PitchShift / 100));
                    entry[40] = byOriginalKey;                                      // byOriginalKey
                    byte chCorrection = (byte)(CalcFineTune(tone.PitchShift) % 100);
                    entry[41] = chCorrection;                                       // chCorrection
                }
                else
                {
                    entry[40] = 255;                                                // byOriginalKey
                    entry[41] = 0;                                                  // chCorrection
                }
                entry[42] = 0;                                                      // wSampleLink
                entry[44] = 1;                                                      // sfSampleType (monoSample)
                bw.Write(entry);
                sampleOffset += length;
                sampleIndex++;
            }
            // add terminal
            bw.Write(new byte[46]);
            return ms.ToArray();
        }
    }

    #endregion

    #region ScaleConversion

    public static double SecondsToTimecents(double secs)
    {
        return Math.Log(secs) / Math.Log(2.0) * 1200.0;
    }

    public static double ConvertLogScaleValToAtten(double percent)
    {
        if (percent == 0)
            return 100.0; // assume 0 is -100.0db attenuation
        double atten = 20 * Math.Log10(percent) * 2;
        return Math.Min(-atten, 100.0);
    }

    /// <summary>
    /// Calculates the Correction from a PitchShift.
    /// </summary>
    public static short CalcFineTune(byte fineTune)
    {
        if (fineTune == 0) return 0;
        double pitchApprox = 0.7824 * fineTune - 0.6061;
        return (short)Math.Round(pitchApprox);
    }

    /// <summary>
    /// Calculates the initialAttenuation from a volume value (0–127).
    /// Returns the result as a short value in 0.01 dB units.
    /// </summary>
    private static short ConvertVolumeToInitialAttenuation(byte volume)
    {
        double x = 127 - volume;
        double dB = 0.008925 * Math.Pow(x, 1.161);
        return (short)Math.Round(dB * 100);
    }

    /// <summary>
    /// Maps a MIDI pan value (0x00–0x7F) to the SF2/DLS range of -50.0 to 50.0.
    /// </summary>
    public static short ConvertPanByte(short pan)
    {
        // If the value is the center (64), return 0.
        if (pan == 64) return 0;
        // Convert the range 0–127 to 0–1 and shift based on the center (0.5).
        double normalized = ((double)pan / 127.0) - 0.5;
        // Multiply by 1000 to express the result in 0.1% units.
        double result = normalized * 1000;
        return (short)Math.Round(result);
    }

    #endregion
}
