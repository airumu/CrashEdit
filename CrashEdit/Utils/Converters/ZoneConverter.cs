using CrashEdit.Crash;
using OpenTK.Mathematics;
using System.Media;
using System.Text.Json;

namespace CrashEdit.CE
{
    public class ZoneConverter
    {
        public class CameraPoint
        {
            public float[] pos { get; set; }
            public float[] rot { get; set; }
        }

        public class ZoneObject
        {
            public string name { get; set; }
            public float[] min { get; set; }
            public float[] max { get; set; }
            public List<List<CameraPoint>> paths { get; set; }
        }

        public class ZoneObjects
        {
            public List<ZoneObject> zones { get; set; }
        }

        public static void Import(string file)
        {
            try
            {
                string json = File.ReadAllText(file);
                ZoneObjects zoneObjs = JsonSerializer.Deserialize<ZoneObjects>(json);

                for (int i = 0; i < zoneObjs.zones.Count; i++)
                {
                    ZoneObject zoneObj = zoneObjs.zones[i];

                    // create zone
                    Vector3 min = new(
                        MathF.Round(zoneObj.min[0]), // X
                        MathF.Round(zoneObj.min[2]), // Z
                        MathF.Round(-zoneObj.max[1]) // -Y (flipped)
                    );
                    Vector3 max = new(
                        MathF.Round(zoneObj.max[0]), // X
                        MathF.Round(zoneObj.max[2]), // Z
                        MathF.Round(-zoneObj.min[1]) // -Y (flipped)
                    );

                    string ename = Entry.EIDToEName(Entry.NullEID);

                    // try to get a zone eid from the object name
                    string str = zoneObj.name;
                    if (((str.Length >= 6 && str[^6] == '_') || str.Length == 5) && str.EndsWith('Z'))
                        ename = str[^5..];

                    ZoneHeader header = ZoneHeader.Load(new byte[0x318]);
                    header.Zones[0] = Entry.ENameToEID(ename);
                    header.ZoneLinkTypes[0] = 0x1E;
                    header.Music = Entry.NullEID;

                    ZoneEntry zone = new(
                        zoneheader: header,
                        layout: new byte[0x28],
                        entities: [],
                        eid: Entry.ENameToEID(ename))
                    {
                        X = (int)(min.X * GameScales.ZoneC1),
                        Y = (int)(min.Y * GameScales.ZoneC1),
                        Z = (int)(min.Z * GameScales.ZoneC1),
                        Width = (int)((max.X - min.X) * GameScales.ZoneC1),
                        Height = (int)((max.Y - min.Y) * GameScales.ZoneC1),
                        Depth = (int)((max.Z - min.Z) * GameScales.ZoneC1),

                        InfoCount = 2,
                        ZoneCount = 1
                    };

                    // create cameras
                    for (int j = 0; j < zoneObj.paths.Count; j++)
                    {
                        List<CameraPoint> path = zoneObj.paths[j];

                        Entity cam0 = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
                        Entity cam1 = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
                        Entity cam2 = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());

                        foreach (var cam in path)
                        {
                            // pos
                            Vector3 pos = new(
                                cam.pos[0], // X
                                cam.pos[2], // Z
                                -cam.pos[1] // -Y
                            );

                            short px = (short)(pos.X * GameScales.ZoneCameraC1);
                            short py = (short)(pos.Y * GameScales.ZoneCameraC1);
                            short pz = (short)(pos.Z * GameScales.ZoneCameraC1);

                            // rot
                            // TODO: verify
                            Vector3 euler = new(
                                cam.rot[0], // X
                                -cam.rot[2], // -Z ?
                                -cam.rot[1]  // -Y ?
                            );

                            const float rad2ang = 2048f / MathF.PI;
                            // TODO: verify
                            float corrected = euler.X - MathF.PI / 2f; // -90deg
                            short rx = (short)(4096f - (-corrected * rad2ang));
                            short ry = (short)(-euler.Y * rad2ang);
                            short rz = (short)(-euler.Z * rad2ang);

                            cam0.Positions.Add(new EntityPosition(px, py, pz));
                            cam1.Positions.Add(new EntityPosition(rx, ry, rz));
                            cam1.Positions.Add(new EntityPosition(rx, ry, rz)); // add twice for now
                        }

                        cam0.CameraIndex = j;
                        cam1.CameraIndex = j;
                        cam2.CameraIndex = j;
                        cam0.CameraSubIndex = 0;
                        cam1.CameraSubIndex = 1;
                        cam2.CameraSubIndex = 2;

                        zone.Entities.Add(cam0);
                        zone.Entities.Add(cam1);
                        zone.Entities.Add(cam2);
                        zone.CameraCount += 3;
                    }

                    string filePath = Path.Combine(Path.GetDirectoryName(file), ename + ".nsentry");
                    File.WriteAllBytes(filePath, zone.Save());
                    Console.WriteLine($"  Exported {filePath}");
                }

                SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
