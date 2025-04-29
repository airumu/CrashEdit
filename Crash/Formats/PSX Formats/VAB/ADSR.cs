namespace CrashEdit.Crash
{
    public class ADSR
    {
        public double AttackTime { get; set; }
        public double HoldTime { get; set; }
        public double DecayTime { get; set; }
        public double SustainTime { get; set; }
        public double SustainLevel { get; set; }
        public double ReleaseTime { get; set; }
    }

    public static class PSXADSR
    {
        private static ulong[] RateTable = new ulong[160];

        public static void InitADSR()
        {
            Array.Clear(RateTable, 0, RateTable.Length);

            ulong r = 3;
            ulong rs = 1;
            ulong rd = 0;
            for (int i = 32; i < 160; i++)
            {
                if (r < 0x3FFFFFFFUL)
                {
                    r += rs;
                    rd++;
                    if (rd == 5)
                    {
                        rd = 1;
                        rs *= 2;
                    }
                }
                if (r > 0x3FFFFFFFUL) r = 0x3FFFFFFFUL;
                RateTable[i] = r;
            }
        }

        private static int RoundToZero(int val)
        {
            return (val < 0) ? 0 : val;
        }

        private static double LinAmpDecayTimeToLinDBDecayTime(double secondsToFullAtten, int linearVolumeRange)
        {
            double expMinDecibel = -100.0;
            double linearMinDecibel = Math.Log10(1.0 / linearVolumeRange) * 20.0;
            double linearToExpScale = Math.Log(linearMinDecibel - expMinDecibel) / Math.Log(2.0);
            return secondsToFullAtten * linearToExpScale;
        }

        public static ADSR ComputeADSR(ushort ADSR1, ushort ADSR2)
        {
            InitADSR();

            byte Am = (byte)((ADSR1 & 0x8000) >> 15);
            byte Ar = (byte)((ADSR1 & 0x7F00) >> 8);
            byte Dr = (byte)((ADSR1 & 0x00F0) >> 4);
            byte Sl = (byte)(ADSR1 & 0x000F);
            byte Rm = (byte)((ADSR2 & 0x0020) >> 5);
            byte Rr = (byte)(ADSR2 & 0x001F);
            byte Sm = (byte)((ADSR2 & 0x8000) >> 15);
            byte Sd = (byte)((ADSR2 & 0x4000) >> 14);
            byte Sr = (byte)((ADSR2 >> 6) & 0x7F);

            double sampleRate = 44100.0;

            ADSR adsr = new ADSR();

            // Attack Time
            if ((Ar ^ 0x7F) < 0x10)
                Ar = 0;
            double samples = 0;
            ulong rate = 0;
            if (Am == 0)
            {
                int index = RoundToZero((Ar ^ 0x7F) - 0x10) + 32;
                rate = RateTable[index];
                samples = Math.Ceiling(0x7FFFFFFF / (double)rate);
            }
            else if (Am == 1)
            {
                int index = RoundToZero((Ar ^ 0x7F) - 0x10) + 32;
                rate = RateTable[index];
                samples = 0x60000000 / (double)rate;
                ulong remainder = 0x60000000UL % rate;
                index = RoundToZero((Ar ^ 0x7F) - 0x18) + 32;
                rate = RateTable[index];
                samples += Math.Ceiling(Math.Max(0, 0x1FFFFFFF - (long)remainder) / (double)rate);
            }
            double attackTime = samples / sampleRate;
            adsr.AttackTime = attackTime;

            // Hold Time
            adsr.HoldTime = 0; // let's just set this to 0

            // Decay Time
            ulong envelope_level = 0x7FFFFFFFUL;
            bool bSustainLevFound = false;
            ulong realSustainLevel = 0;
            int l = 0;
            while (envelope_level > 0)
            {
                int calc = (4 * (Dr ^ 0x1F));
                if (calc < 0x18)
                    Dr = 0;
                int seg = (int)((envelope_level >> 28) & 0x7);
                ulong decrement = 0;
                switch (seg)
                {
                    case 0: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 0) + 32]; break;
                    case 1: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 4) + 32]; break;
                    case 2: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 6) + 32]; break;
                    case 3: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 8) + 32]; break;
                    case 4: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 9) + 32]; break;
                    case 5: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 10) + 32]; break;
                    case 6: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 11) + 32]; break;
                    case 7: decrement = RateTable[RoundToZero((4 * (Dr ^ 0x1F)) - 0x18 + 12) + 32]; break;
                }
                envelope_level = envelope_level > decrement ? envelope_level - decrement : 0;
                l++;
                if (!bSustainLevFound && (((envelope_level >> 27) & 0xF) <= Sl))
                {
                    realSustainLevel = envelope_level;
                    bSustainLevFound = true;
                }
            }
            double decayTime = l / sampleRate;
            adsr.DecayTime = decayTime;

            // Sustain Time
            envelope_level = 0x7FFFFFFFUL;
            double sustainTime = 0;
            if (Sd == 0)
            {
                sustainTime = -1;
            }
            else
            {
                if (Sr == 0x7F)
                {
                    sustainTime = -1;
                }
                else
                {
                    if (Sm == 0)
                    {
                        int index = RoundToZero((Sr ^ 0x7F) - 0x0F) + 32;
                        rate = RateTable[index];
                        sustainTime = Math.Ceiling(0x7FFFFFFF / (double)rate);
                    }
                    else
                    {
                        l = 0;
                        while (envelope_level > 0)
                        {
                            long envelope_level_diff = 0;
                            long envelope_level_target = 0;
                            int seg = (int)((envelope_level >> 28) & 0x7);
                            switch (seg)
                            {
                                case 0: envelope_level_target = 0x00000000; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 0) + 32]; break;
                                case 1: envelope_level_target = 0x0FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 4) + 32]; break;
                                case 2: envelope_level_target = 0x1FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 6) + 32]; break;
                                case 3: envelope_level_target = 0x2FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 8) + 32]; break;
                                case 4: envelope_level_target = 0x3FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 9) + 32]; break;
                                case 5: envelope_level_target = 0x4FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 10) + 32]; break;
                                case 6: envelope_level_target = 0x5FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 11) + 32]; break;
                                case 7: envelope_level_target = 0x6FFFFFFF; envelope_level_diff = (long)RateTable[RoundToZero((Sr ^ 0x7F) - 0x1B + 12) + 32]; break;
                                default: envelope_level_target = 0; envelope_level_diff = 1; break;
                            }
                            long steps = (long)Math.Ceiling((double)((long)envelope_level - envelope_level_target) / envelope_level_diff);
                            envelope_level = (long)envelope_level > envelope_level_diff * steps ? envelope_level - (ulong)(envelope_level_diff * steps) : 0;
                            l += (int)steps;
                        }
                        sustainTime = l / sampleRate;
                    }
                }
            }
            adsr.SustainTime = (sustainTime == -1) ? -1 : LinAmpDecayTimeToLinDBDecayTime(sustainTime, 0x800);

            // Sustain Level
            if (Sl == 0)
                realSustainLevel = 0x07FFFFFFUL;
            adsr.SustainLevel = realSustainLevel / (double)0x7FFFFFFFUL;

            // If decay is going unused, and there's a sustain rate with sustain level close to max...
            //  we'll put the sustain_rate in place of the decay rate.
            if ((adsr.DecayTime < 2 || (Dr == 0x0F && Sl >= 0x0C)) && Sr < 0x7E && Sd == 1)
            {
                // adsr.SustainLevel = 0;
                adsr.SustainLevel = 0x07FFFFFFUL;
                adsr.DecayTime = adsr.SustainTime;
            }

            // Release Time
            envelope_level = 0x7FFFFFFFUL;
            // dirty hack
            {
                int index = RoundToZero((4 * (Rr ^ 0x1F)) - 0x0C) + 32;
                rate = RateTable[index];
                if (rate != 0)
                    samples = Math.Ceiling(envelope_level / (double)rate);
                else
                    samples = 0;

               
                if (Rm == 1)
                {
                    samples *= Math.E;
                }
            }
            {
                //if (Rm == 0)
                //{
                //    int index = RoundToZero((4 * (Rr ^ 0x1F)) - 0x0C) + 32;
                //    rate = RateTable[index];
                //    if (rate != 0)
                //        samples = Math.Ceiling(envelope_level / (double)rate);
                //    else
                //        samples = 0;
                //}
                //else if (Rm == 1)
                //{
                //    if ((Rr ^ 0x1F) * 4 < 0x18)
                //        Rr = 0;
                //    Console.WriteLine($"Rr: {Rr}");

                //    l = 0;
                //    int maxIterations = 10000;
                //    while (envelope_level > 0 && l < maxIterations)
                //    {
                //        int seg = (int)((envelope_level >> 28) & 0x7);
                //        Console.WriteLine(seg);
                //        int offset = 0;
                //        switch (seg)
                //        {
                //            case 0: offset = 0; break;
                //            case 1: offset = 4; break;
                //            case 2: offset = 6; break;
                //            case 3: offset = 8; break;
                //            case 4: offset = 9; break;
                //            case 5: offset = 10; break;
                //            case 6: offset = 11; break;
                //            case 7: offset = 12; break;
                //        }
                //        int index = RoundToZero((4 * (Rr ^ 0x1F)) - 0x18 + offset) + 32;
                //        ulong decrement = RateTable[index];
                //        if (decrement == 0) decrement = 1;
                //        envelope_level = envelope_level > decrement ? envelope_level - decrement : 0;
                //        Console.WriteLine($"l[{l}]: seg {seg}, decrement {decrement}, envelope_level {envelope_level}");
                //        l++;
                //    }
                //    if (l >= maxIterations)
                //        samples = 0;
                //    else
                //        samples = l;
                //}
            }
            double releaseTime = samples / sampleRate;
            adsr.ReleaseTime = LinAmpDecayTimeToLinDBDecayTime(releaseTime, 0x800);

            return adsr;
        }
    }
}
