namespace CrashEdit.Crash
{
    public sealed class NSDSpawnPoint
    {
        public NSDSpawnPoint(int zoneeid, int camera, int point, int spawnx, int spawny, int spawnz)
        {
            ZoneEID = zoneeid;
            Camera = camera;
            Point = point;
            SpawnX = spawnx;
            SpawnY = spawny;
            SpawnZ = spawnz;
        }

        public int ZoneEID { get; set; }
        public int Camera { get; set; }
        public int Point { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }
    }
}
