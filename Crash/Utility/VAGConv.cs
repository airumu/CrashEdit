using System.Runtime.InteropServices;
using System.Text;

// Based on code by GlaireDaggers
// https://github.com/GlaireDaggers/PSX-VAG-.NET/blob/main/src/VAGWriter.cs
namespace CrashEdit.Crash
{
    public class VAGConv : IDisposable
    {
        private const int BYTES_PER_FRAME = 0x10;
        private const int SAMPLES_PER_FRAME = (BYTES_PER_FRAME - 0x02) * 2;
        private const int FRAME_START_OFFSET = 0x30;

        private static readonly int[] filter_k1 = [0, 60, 115, 98, 122];
        private static readonly int[] filter_k2 = [0, 0, -52, -55, -60];

        private struct EncoderState
        {
            public ulong mse;
            public int prev1, prev2;
        }

        private readonly BinaryWriter _writer;

        private long _bytesPerChannelFieldOffset;
        private byte _lastShift;

        private List<short> _samples;
        private EncoderState _encoderStates;

        private readonly int _samplerate;
        private readonly int _channelCount;

        private readonly byte[] _preBuf = new byte[28];
        private readonly short[] _dummyFrameSamples = new short[28];
        private readonly byte[] _frameTmp = new byte[BYTES_PER_FRAME];

        public VAGConv(int samplerate, int channelCount, short[] sampleData, BinaryWriter writter)
        {
            if (channelCount != 1)
            {
                throw new Exception("Only mono audio is supported for VAG encoding");
            }

            if (samplerate <= 0)
            {
                throw new Exception("Sample rate must be a positive integer");
            }

            _writer = writter;
            _samples = [.. sampleData];
            _encoderStates = new();
            _samplerate = samplerate;
            _channelCount = channelCount;
        }

        public void WriteHeader()
        {
            int samplerate = _samplerate;
            int channelCount = _channelCount;
            int chunkSize = 0;

            // VAG header
            _writer.Write(Encoding.ASCII.GetBytes("VAGp"));

            // version (big endian)
            _writer.Write((byte)0x00);
            _writer.Write((byte)0x00);
            _writer.Write((byte)0x00);
            _writer.Write((byte)0x20);

            _writer.Write((byte)chunkSize);
            _writer.Write((byte)(chunkSize >> 8));
            _writer.Write((byte)(chunkSize >> 16));
            _writer.Write((byte)(chunkSize >> 24));

            // length of data per channel (big endian)
            // store position so we can go back and rewrite this later
            _bytesPerChannelFieldOffset = _writer.BaseStream.Position;
            _writer.Write((uint)0);

            // samplerate (big endian)
            _writer.Write((byte)(samplerate >> 24));
            _writer.Write((byte)(samplerate >> 16));
            _writer.Write((byte)(samplerate >> 8));
            _writer.Write((byte)samplerate);

            // skip 10 bytes
            _writer.BaseStream.Seek(10, SeekOrigin.Current);

            // number of channels (little-endian)
            _writer.Write((ushort)channelCount);

            // 16 padding bytes
            _writer.BaseStream.Seek(16, SeekOrigin.Current);
        }

        public void Finish()
        {
            int frames = _samples.Count / SAMPLES_PER_FRAME;

            if (_samples.Count % SAMPLES_PER_FRAME != 0)
                frames++;

            for (int i = 0; i < frames; i++)
            {
                var srcOffset = i * SAMPLES_PER_FRAME;

                byte flags = 0;

                // last frame has end+mute set
                if (i == frames - 1)
                {
                    flags = 1;
                }

                if (srcOffset >= _samples.Count)
                {
                    // encode dummy padding frame
                    EncodeFrame(flags, ref _encoderStates, _dummyFrameSamples, _frameTmp);
                }
                else
                {
                    EncodeFrame(flags,
                        ref _encoderStates,
                        CollectionsMarshal.AsSpan(_samples)[srcOffset..],
                        _frameTmp);
                }

                _writer.Write(_frameTmp);
            }

            WriteFooter();
            NullifyFirstFrame();
            WriteSampleLength((uint)(frames * BYTES_PER_FRAME));
        }

        public void WriteSampleLength(uint length)
        {
            // patch up bytes per channel field in header
            long pos = _writer.BaseStream.Position;
            _writer.BaseStream.Seek(_bytesPerChannelFieldOffset, SeekOrigin.Begin);
            _writer.Write((byte)(length >> 24));
            _writer.Write((byte)(length >> 16));
            _writer.Write((byte)(length >> 8));
            _writer.Write((byte)length);
            _writer.BaseStream.Seek(pos, SeekOrigin.Begin);
        }

        private void NullifyFirstFrame()
        {
            // nullify first frame (first 16 bytes of data)
            long pos = _writer.BaseStream.Position;
            _writer.BaseStream.Seek(FRAME_START_OFFSET, SeekOrigin.Begin);
            for (int i = 0; i < 16; i++)
            {
                _writer.Write((byte)0);
            }
            _writer.BaseStream.Seek(pos, SeekOrigin.Begin);
        }

        private void WriteFooter()
        {
            // write end frame
            _writer.Write(_lastShift);
            _writer.Write((byte)0x07);

            // write 14 padding bytes to make the file end on a 16 byte boundary
            for (int i = 0; i < 14; i++)
            {
                _writer.Write((byte)0);
            }
        }

        private void EncodeFrame(byte flags, ref EncoderState state, Span<short> samples, Span<byte> data)
        {
            data[0] = EncodeFrame(ref state, samples, _preBuf, 0, 5, 12);
            data[1] = flags;

            for (int i = 0; i < 28; i += 2)
            {
                data[2 + (i >> 1)] = (byte)((_preBuf[i] & 0x0F) | (_preBuf[i + 1] << 4));
            }

            _lastShift = data[0];
        }

        private static byte EncodeFrame(ref EncoderState state, Span<short> samples, Span<byte> data, int dataShift, int filterCount, int shiftRange)
        {
            ulong bestMse = 1UL << 50;
            int bestFilter = 0;
            int bestSampleShift = 0;

            for (int filter = 0; filter < filterCount; filter++)
            {
                int trueMinShift = FindMinShift(state, samples, filter, shiftRange);

                int minShift = trueMinShift - 1;
                int maxShift = trueMinShift + 1;
                if (minShift < 0) minShift = 0;
                if (maxShift > shiftRange) maxShift = shiftRange;

                for (int sampleShift = minShift; sampleShift <= maxShift; sampleShift++)
                {
                    TryEncodeFrame(state, out EncoderState proposed, samples, data, dataShift, filter, sampleShift, shiftRange);

                    if (bestMse > proposed.mse)
                    {
                        bestMse = proposed.mse;
                        bestFilter = filter;
                        bestSampleShift = sampleShift;
                    }
                }
            }

            // use best encoder settings
            return TryEncodeFrame(state, out state, samples, data, dataShift, bestFilter, bestSampleShift, shiftRange);
        }

        private static int FindMinShift(in EncoderState state, Span<short> samples, int filter, int shiftRange)
        {
            int prev1 = state.prev1;
            int prev2 = state.prev2;
            int k1 = filter_k1[filter];
            int k2 = filter_k2[filter];

            int rightShift = 0;

            int min = 0;
            int max = 0;

            for (int i = 0; i < 28; i++)
            {
                int rawSample = i >= samples.Length ? 0 : samples[i];
                int previousValues = (k1 * prev1 + k2 * prev2 + (1 << 5)) >> 6;
                int sample = rawSample - previousValues;

                if (sample < min) { min = sample; }
                if (sample > max) { max = sample; }

                prev2 = prev1;
                prev1 = rawSample;
            }

            while (rightShift < shiftRange && (max >> rightShift) > (+0x7FFF >> shiftRange)) { rightShift++; }
            ;
            while (rightShift < shiftRange && (min >> rightShift) < (-0x8000 >> shiftRange)) { rightShift++; }
            ;

            int minShift = shiftRange - rightShift;

            return minShift;
        }

        private static byte TryEncodeFrame(in EncoderState inState, out EncoderState outState, Span<short> samples, Span<byte> data, int dataShift, int filter, int sampleShift, int shiftRange)
        {
            byte sampleMask = (byte)(0xFFFF >> shiftRange);
            byte nondataMask = (byte)~(sampleMask << dataShift);

            int minShift = sampleShift;
            int k1 = filter_k1[filter];
            int k2 = filter_k2[filter];

            byte hdr = (byte)((minShift & 0x0F) | (filter << 4));

            outState = inState;
            outState.mse = 0;

            for (int i = 0; i < 28; i++)
            {
                int sample = i >= samples.Length ? 0 : samples[i];
                int previousValues = (k1 * outState.prev1 + k2 * outState.prev2 + (1 << 5)) >> 6;
                int sampleEnc = sample - previousValues;
                sampleEnc <<= minShift;
                sampleEnc += 1 << (shiftRange - 1);
                sampleEnc >>= shiftRange;
                if (sampleEnc < (-0x8000 >> shiftRange)) { sampleEnc = -0x8000 >> shiftRange; }
                if (sampleEnc > (+0x7FFF >> shiftRange)) { sampleEnc = +0x7FFF >> shiftRange; }
                sampleEnc &= sampleMask;

                int sampleDec = (short)((sampleEnc & sampleMask) << shiftRange);
                sampleDec >>= minShift;
                sampleDec += previousValues;
                if (sampleDec > +0x7FFF) { sampleDec = +0x7FFF; }
                if (sampleDec < -0x8000) { sampleDec = -0x8000; }

                long sampleError = sampleDec - sample;

                data[i] = (byte)((data[i] & nondataMask) | (sampleEnc << dataShift));

                outState.mse += (ulong)sampleError * (ulong)sampleError;
                outState.prev2 = outState.prev1;
                outState.prev1 = sampleDec;
            }

            return hdr;
        }

        public void Dispose() => _writer.Dispose();
    }
}
