namespace CrashEdit.Crash
{
    public interface ModelStruct
    {
    }

    public class ModelTriangle : ModelStruct
    {
        public const byte NullPtr = 0x57;

        public enum IndexType : byte
        {
            Original = 0,
            Duplicate = 1
        }

        public static ModelTriangle Load(uint structure)
        {
            // LE format: YYSSFTLL PPPPPPPP CCCCCCCA XXXXXXXX
            // TTL is probably TLL or maybe even TPP?
            byte texture = (byte)structure; // X
            bool animated = (structure >> 8 & 0x1) != 0; // A
            byte color = (byte)(structure >> 9 & 0x7F); // C
            byte key = (byte)(structure >> 16); // P
            byte unknown = (byte)(structure >> 24 & 0x3); // L
            byte type = (byte)(structure >> 26 & 0x01); // T
            bool flag = (structure >> 27 & 0x1) != 0; // F
            byte tritype = (byte)(structure >> 28); // Y/S
            return new ModelTriangle(texture, animated, color, key, unknown, type, flag, tritype);
        }

        public ModelTriangle(byte texture, bool animated, byte color, byte key, byte unknown, byte type, bool flag, byte tritype)
        {
            TextureIndex = texture;
            Animated = animated;
            ColorIndex = color;
            PositionKey = key;
            Unknown = unknown;
            Type = (IndexType)type;
            Flag = flag;
            TriangleSubtype = (byte)(tritype & 0x3);
            TriangleType = (byte)(tritype >> 2 & 0x3);
        }

        public byte TextureIndex { get; set; }
        public byte ColorIndex { get; set; }
        public bool Animated { get; set; }
        public byte PositionKey { get; set; }
        public byte TriangleType { get; set; }
        public byte TriangleSubtype { get; set; }
        public byte Unknown { get; set; }
        public bool Flag { get; set; }
        public IndexType Type { get; set; }

        public uint Save()
        {
            uint structure = 0;
            structure |= (uint)TextureIndex; // X
            structure |= (uint)(Animated ? 1 : 0) << 8; // A
            structure |= (uint)(ColorIndex & 0x7F) << 9; // C
            structure |= (uint)PositionKey << 16; // P
            structure |= (uint)(Unknown & 0x3) << 24; // L
            structure |= (uint)((byte)Type & 0x01) << 26; // T
            structure |= (uint)(Flag ? 1 : 0) << 27; // F
            structure |= (uint)(TriangleSubtype & 0x3) << 28; // S
            structure |= (uint)(TriangleType & 0x3) << 30; // Y
            return structure;
        }
    }

    public struct ModelColor : ModelStruct
    {
        public static ModelColor Load(uint structure)
        {
            byte color1 = (byte)(structure >> 2 & 0x7F);
            byte color2 = (byte)(structure >> 9 & 0x7F);
            return new ModelColor(color1, color2);
        }

        public ModelColor(byte color1, byte color2)
        {
            Color1 = color1;
            Color2 = color2;
        }

        public byte Color1 { get; set; }
        public byte Color2 { get; set; }

        public uint Save()
        {
            uint structure = 0;
            structure |= (uint)(Color1 & 0x7F) << 2;
            structure |= (uint)(Color2 & 0x7F) << 9;
            return structure;
        }

        public uint SaveHeader()
        {
            uint structure = Color1;
            return structure;
        }
    }
}
