namespace CrashEdit.Crash
{
    public class ModelExtendedTexture
    {
        public static ModelExtendedTexture Load(byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);
            if (data.Length != 4)
                throw new ArgumentException("Value must be 4 bytes long.", nameof(data));
            int rest = BitConv.FromInt32(data, 0);
            return new ModelExtendedTexture(rest);
        }

        public ModelExtendedTexture(int data)
        {
            Data = data;
        }

        public int Data { get; set; }
        public int Offset
        {
            get => Data & 0x7FF;
            set => Data = (Data & ~0x7FF) | (value & 0x7FF);
        }
        public bool IsLOD
        {
            get => (Data & (1 << 31)) != 0;
            set
            {
                if (value)
                {
                    Data |= (1 << 31);
                }
                else
                {
                    Data &= ~(1 << 31);
                }
            }
        }

        public int LOD0
        {
            get => IsLOD ? (Data >> 29) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 29)) | ((value & 0x3) << 29);
            }
        }
        public int LOD1
        {
            get => IsLOD ? (Data >> 27) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 27)) | ((value & 0x3) << 27);
            }
        }
        public int LOD2
        {
            get => IsLOD ? (Data >> 25) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 25)) | ((value & 0x3) << 25);
            }
        }
        public int LOD3
        {
            get => IsLOD ? (Data >> 23) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 23)) | ((value & 0x3) << 23);
            }
        }
        public int LOD4
        {
            get => IsLOD ? (Data >> 21) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 21)) | ((value & 0x3) << 21);
            }
        }
        public int LOD5
        {
            get => IsLOD ? (Data >> 19) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 19)) | ((value & 0x3) << 19);
            }
        }
        public int LOD6
        {
            get => IsLOD ? (Data >> 17) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 17)) | ((value & 0x3) << 17);
            }
        }
        public int LOD7
        {
            get => IsLOD ? (Data >> 15) & 0x3 : throw new Exception("This extended texture field is not a LOD field.");
            set
            {
                if (!IsLOD) throw new Exception("This extended texture field is not a LOD field.");
                Data = (Data & ~(0x3 << 15)) | ((value & 0x3) << 15);
            }
        }

        public bool Leap
        {
            get => !IsLOD ? ((Data >> 11) & 0x1) == 1 : throw new Exception("This extended texture field is not a flipbook field.");
            set
            {
                if (IsLOD) throw new Exception("This extended texture field is not a flipbook field.");
                Data = value ? (Data | (1 << 11)) : (Data & ~(1 << 11));
            }
        }
        public int Mask
        {
            get => !IsLOD ? (Data >> 12) & 0x7F : throw new Exception("This extended texture field is not a flipbook field.");
            set
            {
                if (IsLOD) throw new Exception("This extended texture field is not a flipbook field.");
                Data = (Data & ~(0x7F << 12)) | ((value & 0x7F) << 12);
            }
        }
        public int Delay
        {
            get => !IsLOD ? (Data >> 19) & 0x7F : throw new Exception("This extended texture field is not a flipbook field.");
            set
            {
                if (IsLOD) throw new Exception("This extended texture field is not a flipbook field.");
                Data = (Data & ~(0x7F << 19)) | ((value & 0x7F) << 19);
            }
        }
        public int Latency
        {
            get => !IsLOD ? (Data >> 26) & 0x1F : throw new Exception("This extended texture field is not a flipbook field.");
            set
            {
                if (IsLOD) throw new Exception("This extended texture field is not a flipbook field.");
                Data = (Data & ~(0x1F << 26)) | ((value & 0x1F) << 26);
            }
        }

        public byte[] Save()
        {
            byte[] result = new byte[4];
            BitConv.ToInt32(result, 0, Data);
            return result;
        }
    }
}
