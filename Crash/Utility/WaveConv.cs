using System.Text;

namespace CrashEdit.Crash
{
    public static class WaveConv
    {
        public static RIFF ToWave(byte[] data, int samplerate)
        {
            ArgumentNullException.ThrowIfNull(data);
            byte[] format = new byte[18];
            BitConv.ToInt16(format, 0, 1);
            BitConv.ToInt16(format, 2, 1);
            BitConv.ToInt32(format, 4, samplerate);
            BitConv.ToInt32(format, 8, samplerate * 2);
            BitConv.ToInt16(format, 12, 2);
            BitConv.ToInt16(format, 14, 16);
            BitConv.ToInt16(format, 16, 0);
            RIFF wave = new RIFF("wave");
            wave.Items.Add(new RIFFData("fmt ", format));
            wave.Items.Add(new RIFFData("data", data));
            return wave;
        }

        public static RIFF ToWave(byte[] data, int samplerate, string sampleName)
        {
            ArgumentNullException.ThrowIfNull(data);
            byte[] format = new byte[18];
            BitConv.ToInt16(format, 0, 1);
            BitConv.ToInt16(format, 2, 1);
            BitConv.ToInt32(format, 4, samplerate);
            BitConv.ToInt32(format, 8, samplerate * 2);
            BitConv.ToInt16(format, 12, 2);
            BitConv.ToInt16(format, 14, 16);
            BitConv.ToInt16(format, 16, 0);
            RIFF wave = new RIFF("wave");
            wave.Items.Add(new RIFFData("fmt ", format));
            wave.Items.Add(new RIFFData("data", data));
            RIFF info = new RIFF("INFO");
            StringBuilder name = new StringBuilder(sampleName);
            byte[] inamData = Encoding.ASCII.GetBytes(RIFF.AlignName(name).ToString());
            info.Items.Add(new RIFFData("INAM", inamData));
            wave.Items.Add(info);
            return wave;
        }


    }
}
