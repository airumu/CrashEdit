using System.Collections.Generic;

namespace CrashEdit.Crash
{
    public sealed class ZoneHeader : IResource
    {
        public static ZoneHeader Load(byte[] data)
        {
            if (data.Length != 0x318)
            {
                ErrorManager.SignalError("Zone header must be 0x318 bytes long.");
            }

            int worldCount = BitConv.FromInt32(data, 0);
            List<int> worlds = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                worlds.Add(BitConv.FromInt32(data, 0x4 + i * 0x30));
            }
            int infoCount = BitConv.FromInt32(data, 0x184);
            int cameraCount = BitConv.FromInt32(data, 0x188);
            int entityCount = BitConv.FromInt32(data, 0x18C);
            int zoneCount = BitConv.FromInt32(data, 0x190);
            List<int> zones = new List<int>();
            List<int> zoneLinkTypes = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                zones.Add(BitConv.FromInt32(data, 0x194 + i * 0x4));
                zoneLinkTypes.Add(BitConv.FromInt32(data, 0x1B4 + i * 0x4));
            }
            // chunk 1
            byte[] chunk1 = new byte[0x29C - 0x1D4];
            Array.Copy(data, 0x1D4, chunk1, 0, chunk1.Length);
            int zoneFlags = BitConv.FromInt32(data, 0x29C);
            int unk0x2A0 = BitConv.FromInt32(data, 0x2A0);
            int music = BitConv.FromInt32(data, 0x2A4);
            // chunk 2
            byte[] chunk2 = new byte[0x318 - 0x2A8];
            Array.Copy(data, 0x2A8, chunk2, 0, chunk2.Length);
            return new ZoneHeader(worldCount, worlds, infoCount, cameraCount, entityCount, zoneCount, zones, zoneLinkTypes, chunk1, zoneFlags, unk0x2A0, music, chunk2, data, false);
        }

        public static ZoneHeader LoadNew(byte[] data)
        {
            if (data.Length != 0x358)
            {
                ErrorManager.SignalError("Zone header must be 0x358 bytes long.");
            }

            int worldCount = BitConv.FromInt32(data, 0);
            List<int> worlds = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                worlds.Add(BitConv.FromInt32(data, 0x4 + i * 0x30));
            }
            int infoCount = BitConv.FromInt32(data, 0x184);
            int cameraCount = BitConv.FromInt32(data, 0x188);
            int entityCount = BitConv.FromInt32(data, 0x18C);
            int zoneCount = BitConv.FromInt32(data, 0x190);
            List<int> zones = new List<int>();
            List<int> zoneLinkTypes = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                zones.Add(BitConv.FromInt32(data, 0x194 + i * 0x4));
                zoneLinkTypes.Add(BitConv.FromInt32(data, 0x1D4 + i * 0x4));
            }
            // chunk 1
            byte[] chunk1 = new byte[0x2DC - 0x214];
            Array.Copy(data, 0x214, chunk1, 0, chunk1.Length);
            int zoneFlags = BitConv.FromInt32(data, 0x2DC);
            int unk0x2A0 = BitConv.FromInt32(data, 0x2E0);
            int music = BitConv.FromInt32(data, 0x2E4);
            // chunk 2
            byte[] chunk2 = new byte[0x358 - 0x2E8];
            Array.Copy(data, 0x2E8, chunk2, 0, chunk2.Length);
            return new ZoneHeader(worldCount, worlds, infoCount, cameraCount, entityCount, zoneCount, zones, zoneLinkTypes, chunk1, zoneFlags, unk0x2A0, music, chunk2, data, true);
        }

        public ZoneHeader(int worldCount, List<int> worlds, int infoCount, int cameraCount, int entityCount, int zoneCount, List<int> zones, List<int> zoneLinkTypes, byte[] chunk1, int zoneFlags, int unk0x2A0, int music, byte[] chunk2, byte[] data, bool isNew)
        {
            WorldCount = worldCount;
            Worlds = worlds;
            InfoCount = infoCount;
            CameraCount = cameraCount;
            EntityCount = entityCount;
            ZoneCount = zoneCount;
            Zones = zones;
            ZoneLinkTypes = zoneLinkTypes;
            Chunk1 = chunk1;
            ZoneFlags = zoneFlags;
            Unk0x2A0 = unk0x2A0;
            Music = music;
            Chunk2 = chunk2;
            Data = data;
            IsNew = isNew;
        }

        public  string Title => "Header";
        public string ImageKey => "Arrow";

        public int WorldCount { get; set; }
        public List<int> Worlds { get; set; }
        public int InfoCount { get; set; }
        public int CameraCount { get; set; }
        public int EntityCount { get; set; }
        public int ZoneCount { get; set; }
        public List<int> Zones { get; set; }
        public List<int> ZoneLinkTypes { get; set; }
        public byte[] Chunk1 { get; set; }
        public int ZoneFlags { get; set; }
        public int Unk0x2A0 { get; set; }
        public int Music { get; set; }
        public byte[] Chunk2 { get; set; }

        public byte[] Data { get; set; }

        public bool IsNew { get; }

        public byte[] Save()
        {
            if (IsNew)
                return SaveC3();
            else
                return SaveC2();
        }

        public byte[] SaveC2()
        {
            byte[] result = new byte[0x318];
            BitConv.ToInt32(result, 0, WorldCount);
            for (int i = 0; i < 8; i++)
            {
                BitConv.ToInt32(result, 0x4 + i * 0x30, Worlds[i]);
            }
            BitConv.ToInt32(result, 0x184, InfoCount);
            BitConv.ToInt32(result, 0x188, CameraCount);
            BitConv.ToInt32(result, 0x18C, EntityCount);
            BitConv.ToInt32(result, 0x190, ZoneCount);
            for (int i = 0; i < 8; i++)
            {
                BitConv.ToInt32(result, 0x194 + i * 0x4, Zones[i]);
                BitConv.ToInt32(result, 0x1B4 + i * 0x4, ZoneLinkTypes[i]);
            }
            // chunk 1
            Array.Copy(Chunk1, 0, result, 0x1D4, Chunk1.Length);
            BitConv.ToInt32(result, 0x29C, ZoneFlags);
            BitConv.ToInt32(result, 0x2A0, Unk0x2A0);
            BitConv.ToInt32(result, 0x2A4, Music);
            // chunk 2
            Array.Copy(Chunk2, 0, result, 0x2A8, Chunk2.Length);
            return result;
        }

        public byte[] SaveC3()
        {
            byte[] result = new byte[0x358];
            BitConv.ToInt32(result, 0, WorldCount);
            for (int i = 0; i < 8; i++)
            {
                BitConv.ToInt32(result, 0x4 + i * 0x30, Worlds[i]);
            }
            BitConv.ToInt32(result, 0x184, InfoCount);
            BitConv.ToInt32(result, 0x188, CameraCount);
            BitConv.ToInt32(result, 0x18C, EntityCount);
            BitConv.ToInt32(result, 0x190, ZoneCount);
            for (int i = 0; i < 16; i++)
            {
                BitConv.ToInt32(result, 0x194 + i * 0x4, Zones[i]);
                BitConv.ToInt32(result, 0x1D4 + i * 0x4, ZoneLinkTypes[i]);
            }
            // chunk 1
            Array.Copy(Chunk1, 0, result, 0x214, Chunk1.Length);
            BitConv.ToInt32(result, 0x2DC, ZoneFlags);
            BitConv.ToInt32(result, 0x2E0, Unk0x2A0);
            BitConv.ToInt32(result, 0x2E4, Music);
            // chunk 2
            Array.Copy(Chunk2, 0, result, 0x2E8, Chunk2.Length);
            return result;
        }
    }
}
