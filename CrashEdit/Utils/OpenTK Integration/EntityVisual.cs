using System.Text.Json;

namespace CrashEdit.CE
{
    internal sealed record EntityVisual
    {
        public string AnimName;
        public int AnimFrame;

        public EntityVisual(string name, int frame = -1)
        {
            AnimName = name;
            AnimFrame = frame;
        }

        public static EntityVisualList MapCrash1 = [];
        public static EntityVisualList MapCrash2 = [];
        public static EntityVisualList MapCrash3 = [];

        static EntityVisual()
        {
            // default visuals
            MapCrash1.AddVisual(0, 0, new("WiS1V")); // willy
            MapCrash1.AddVisual(1, 2, new("Mo1fV", 0)); // monkey
            MapCrash1.AddVisual(8, 0, new("PoD1V")); // power door
            MapCrash1.AddVisual(8, 1, new("PoD2V")); // power door double left
            MapCrash1.AddVisual(8, 1 + 1000, new("PoD3V")); // power door double right
            MapCrash1.AddVisual(8, 3, new("PoD5V")); // power door double 2 left
            MapCrash1.AddVisual(8, 3 + 1000, new("PoD6V")); // power door double 2 right
            MapCrash1.AddVisual(8, 5, new("PoD4V")); // power door 2
            MapCrash1.AddVisual(8, 6, new("PoD1V")); // locked power door
            MapCrash1.AddVisual(9, 0, new("PRSSV")); // power robot
            MapCrash1.AddVisual(9, 5, new("Psr5V")); // power survey robot
            MapCrash1.AddVisual(9, 6, new("Psr5V")); // power survey robot
            MapCrash1.AddVisual(10, 0, new("PRESV")); // power robot enemy
            MapCrash1.AddVisual(12, 0, new("SliMV")); // slim
            MapCrash1.AddVisual(14, 0, new("PoSpV", 0)); // power spring
            MapCrash1.AddVisual(17, 0, new("FaS1V")); // fat
            MapCrash1.AddVisual(19, 0, new("Tu1iV")); // turtle
            MapCrash1.AddVisual(22, 6, new("JuRcV")); // jungle roller
            MapCrash1.AddVisual(22, 12, new("JuSeV")); // jungle stone
            MapCrash1.AddVisual(22, 13, new("JB4eV")); // jungle barricade high small
            MapCrash1.AddVisual(22, 14, new("JB5eV")); // jungle barricade high medium
            MapCrash1.AddVisual(22, 15, new("JB6eV")); // jungle barricade high large
            MapCrash1.AddVisual(22, 16, new("JB1eV")); // jungle barricade low small
            MapCrash1.AddVisual(22, 17, new("JB2eV")); // jungle barricade low medium
            MapCrash1.AddVisual(22, 18, new("JB3eV")); // jungle barricade low large
            MapCrash1.AddVisual(25, 0, new("JuPiV")); // jungle plant
            MapCrash1.AddVisual(27, 0, new("JuOWV")); // jungle ocean wave
            MapCrash1.AddVisual(28, 1, new("Rl1fV")); // river leaf
            MapCrash1.AddVisual(28, 2, new("RB1fV")); // river branch
            MapCrash1.AddVisual(28, 5, new("RF1fV")); // river fish
            MapCrash1.AddVisual(28, 6, new("Rv1fV")); // river venus
            MapCrash1.AddVisual(28, 7, new("RV1fV")); // river venus
            MapCrash1.AddVisual(31, 0, new("Cr19V")); // crab
            MapCrash1.AddVisual(32, 1, new("WWP0V")); // warp out
            MapCrash1.AddVisual(33, 0, new("WP1iV")); // wall plat
            MapCrash1.AddVisual(33, 1, new("WS1iV")); // wall shield
            MapCrash1.AddVisual(33, 3, new("SL1iV")); // spike log
            MapCrash1.AddVisual(33, 4, new("SL1iV")); // spike log
            MapCrash1.AddVisual(33, 5, new("WT1iV", 0)); // wall torch
            MapCrash1.AddVisual(34, 0, new("BT10V")); // box tnt
            MapCrash1.AddVisual(34, 2, new("BN10V")); // box empty
            MapCrash1.AddVisual(34, 3, new("BS10V", 0)); // box spring
            MapCrash1.AddVisual(34, 4, new("BC10V", 0)); // box continue
            MapCrash1.AddVisual(34, 5, new("BI10V")); // box iron
            MapCrash1.AddVisual(34, 6, new("BF10V", 0)); // box fruit
            MapCrash1.AddVisual(34, 7, new("BA10V", 0)); // box action
            MapCrash1.AddVisual(34, 8, new("BL10V")); // box life
            MapCrash1.AddVisual(34, 9, new("BD10V")); // box doctor
            MapCrash1.AddVisual(34, 10, new("Bp10V")); // box pickup
            MapCrash1.AddVisual(34, 11, new("BP10V")); // box pow
            MapCrash1.AddVisual(34, 13, new("BG10V")); // box ghost
            MapCrash1.AddVisual(34, 15, new("BS20V", 0)); // box iron spring
            MapCrash1.AddVisual(34, 16, new("BT10V")); // box tnt (auto grav)
            MapCrash1.AddVisual(34, 17, new("Bp10V")); // box pickup (auto grav)
            MapCrash1.AddVisual(34, 19, new("BG10V")); // box ghost iron
            MapCrash1.AddVisual(34, 20, new("BN10V")); // box empty (auto grav)
            MapCrash1.AddVisual(38, 0, new("Na1iV")); // native
            MapCrash1.AddVisual(38, 1, new("Na1iV")); // native
            MapCrash1.AddVisual(58, 0, new("Gc10V")); // gem clear
            MapCrash1.AddVisual(58, 1, new("Ge20V")); // gem red
            MapCrash1.AddVisual(58, 2, new("Ge10V")); // gem blue
            MapCrash1.AddVisual(58, 3, new("Ge50V")); // gem green
            MapCrash1.AddVisual(58, 4, new("Ge40V")); // gem purple
            MapCrash1.AddVisual(58, 5, new("Ge60V")); // gem yellow
            MapCrash1.AddVisual(58, 6, new("Ge30V")); // gem orange


            MapCrash2.AddVisual(0, 0, new("Cr10V")); // crash
            MapCrash2.AddVisual(1, 1, new("WWP0V")); // warp out
            MapCrash2.AddVisual(1, 7 + 0000, new("WGB0V", 2)); // warp gate bottom 1
            MapCrash2.AddVisual(1, 8 + 0000, new("WGb0V", 2)); // warp gate bottom exit 1
            MapCrash2.AddVisual(1, 7 + 0900, new("WGT0V", 2)); // warp gate top 1
            MapCrash2.AddVisual(1, 8 + 0900, new("WGt0V", 2)); // warp gate top exit 1
            MapCrash2.AddVisual(1, 7 + 1000, new("WGB5V", 2)); // warp gate bottom 2
            MapCrash2.AddVisual(1, 8 + 1000, new("WGb5V", 2)); // warp gate bottom exit 2
            MapCrash2.AddVisual(1, 7 + 1900, new("WGT5V", 2)); // warp gate top 2
            MapCrash2.AddVisual(1, 8 + 1900, new("WGt5V", 2)); // warp gate top exit 2
            MapCrash2.AddVisual(1, 7 + 2000, new("WGBzV", 2)); // warp gate bottom 3
            MapCrash2.AddVisual(1, 8 + 2000, new("WGbzV", 2)); // warp gate bottom exit 3
            MapCrash2.AddVisual(1, 7 + 2900, new("WGTzV", 2)); // warp gate top 3
            MapCrash2.AddVisual(1, 8 + 2900, new("WGtzV", 2)); // warp gate top exit 3
            MapCrash2.AddVisual(1, 7 + 3000, new("WGBdV", 2)); // warp gate bottom 4
            MapCrash2.AddVisual(1, 8 + 3000, new("WGbdV", 2)); // warp gate bottom exit 4
            MapCrash2.AddVisual(1, 7 + 3900, new("WGTdV", 2)); // warp gate top 4
            MapCrash2.AddVisual(1, 8 + 3900, new("WGtdV", 2)); // warp gate top exit 4
            MapCrash2.AddVisual(1, 7 + 4000, new("WGBiV", 2)); // warp gate bottom 5
            MapCrash2.AddVisual(1, 8 + 4000, new("WGbiV", 2)); // warp gate bottom exit 5
            MapCrash2.AddVisual(1, 7 + 4900, new("WGTiV", 2)); // warp gate top 5
            MapCrash2.AddVisual(1, 8 + 4900, new("WGtiV", 2)); // warp gate top exit 5
            MapCrash2.AddVisual(1, 7 + 5000, new("WGBBV", 2)); // warp gate bottom 6
            MapCrash2.AddVisual(1, 8 + 5000, new("WGbBV", 2)); // warp gate bottom exit 6
            MapCrash2.AddVisual(1, 7 + 5900, new("WGTBV", 2)); // warp gate top 6
            MapCrash2.AddVisual(1, 8 + 5900, new("WGtBV", 2)); // warp gate top exit 6
            MapCrash2.AddVisual(2, 0, new("Ts1bV")); // spike turtle
            MapCrash2.AddVisual(2, 5, new("Tu1bV")); // saw turtle
            MapCrash2.AddVisual(3, 24, new("Cry1V")); // crystal
            MapCrash2.AddVisual(3, 25, new("Ge10V")); // gem
            MapCrash2.AddVisual(6, 0, new("Di1bV")); // dive bird
            MapCrash2.AddVisual(7, 0, new("Fa1fV")); // fireface
            MapCrash2.AddVisual(8, 0, new("Sw2bV")); // swarmer jungle
            MapCrash2.AddVisual(8, 0 + 1000, new("Sw2lV")); // swarmer alpine

            MapCrash2.AddVisual(9, 6, new("El1eV", 6)); // elevator snow
            MapCrash2.AddVisual(9, 6 + 0x2, new("El1eV", 6)); // elevator2 snow
            MapCrash2.AddVisual(9, 6 + 0x100, new("El3eV", 6)); // elevator hard snow
            MapCrash2.AddVisual(9, 6 + 0x3D00, new("El1pV", 6)); // elevator gem blue
            MapCrash2.AddVisual(9, 6 + 0x3E00, new("El1pV", 6)); // elevator gem yellow

            MapCrash2.AddVisual(9, 31, new("El2eV", 6)); // elevator bonus snow

            MapCrash2.AddVisual(9, 32, new("El1eV", 6)); // elevator catch snow
            MapCrash2.AddVisual(9, 32 + 0x100, new("El2eV", 6)); // elevator bonus catch snow
            MapCrash2.AddVisual(9, 32 + 0x3E00, new("El1pV", 6)); // elevator catch gem yellow

            MapCrash2.AddVisual(9, 33, new("HG1bV", 0)); // hole gate jungle
            MapCrash2.AddVisual(9, 33 + 0x100, new("HG2bV", 0)); // hole gate bonus jungle

            MapCrash2.AddVisual(9, 35, new("El2eV", 6)); // elevator bonus catch snow

            MapCrash2.AddVisual(9, 39, new("El2eV", 6)); // bonus guard snow

            MapCrash2.AddVisual(10, 0, new("Mo1fV", 0)); // monkey hop
            MapCrash2.AddVisual(11, 0, new("Go1fV")); // boulder gorilla
            MapCrash2.AddVisual(12, 0, new("Do1aV", 0)); // sewer door
            MapCrash2.AddVisual(12, 8, new("Ee1aV")); // eel
            MapCrash2.AddVisual(14, 1, new("Pl1aV")); // drop plat sewer
            MapCrash2.AddVisual(14, 1 + 1000, new("Pl1eV")); // drop plat snow
            MapCrash2.AddVisual(14, 2, new("Pl1pV")); // plat river
            MapCrash2.AddVisual(14, 2 + 1000, new("Pl2gV")); // plat dynamo
            MapCrash2.AddVisual(14, 3 + 0000, new("Pl1fV")); // drop plat
            MapCrash2.AddVisual(14, 3 + 1000, new("Pl2fV")); // drop plat
            MapCrash2.AddVisual(14, 4 + 0000, new("Pl1fV")); // drop plat
            MapCrash2.AddVisual(14, 4 + 1000, new("Pl2fV")); // drop plat
            MapCrash2.AddVisual(14, 6, new("Pl2eV")); // drop plank
            MapCrash2.AddVisual(14, 7, new("Pl2lV")); // plat touch hard alpine
            MapCrash2.AddVisual(14, 8, new("Pl50V")); // plat gem
            MapCrash2.AddVisual(15, 0, new("JuBbV")); // butterfly 1 jungle
            MapCrash2.AddVisual(15, 1, new("JubbV")); // butterfly 2 jungle
            MapCrash2.AddVisual(15, 2, new("JufbV")); // butterfly 3 jungle
            MapCrash2.AddVisual(15, 0 + 1000, new("JuBpV")); // butterfly 1 river
            MapCrash2.AddVisual(15, 1 + 1000, new("JubpV")); // butterfly 2 river
            MapCrash2.AddVisual(15, 2 + 1000, new("JufpV")); // butterfly 3 river
            MapCrash2.AddVisual(15, 10, new("sw1bV")); // swallup jungle
            MapCrash2.AddVisual(16, 0, new("We1aV")); // welder
            //MapCrash2.AddVisual(18, 0, new("Be2lV", 0)); // bees (+ beehive)
            //MapCrash2.AddVisual(18, 0 + 1000, new("Be4lV", 0)); // bees (+ beehive)
            MapCrash2.AddVisual(20, 0, new("Ar1bV")); // armadillo jungle
            MapCrash2.AddVisual(20, 1, new("Ar3bV")); // armadillo naked jungle
            MapCrash2.AddVisual(20, 0 + 1000, new("Ar1lV")); // armadillo alpine
            MapCrash2.AddVisual(20, 1 + 1000, new("Ar3lV")); // armadillo naked alpine
            MapCrash2.AddVisual(21, 1, new("Mw2cV")); // mech hang
            MapCrash2.AddVisual(21, 3, new("Mw1cV")); // mech bob
            MapCrash2.AddVisual(21, 4, new("Mw2cV")); // mech hang
            MapCrash2.AddVisual(21, 5, new("Mw1cV")); // mech bob
            MapCrash2.AddVisual(24, 0, new("Se1eV")); // seal
            MapCrash2.AddVisual(24, 1, new("Se2eV")); // seal
            MapCrash2.AddVisual(25, 0, new("Pe2eV")); // penguin
            MapCrash2.AddVisual(25, 1, new("Pe2eV")); // penguin pulse
            MapCrash2.AddVisual(26, 0 + 1000, new("Pr1fV", 0)); // crumbler plat 1
            MapCrash2.AddVisual(26, 0 + 2000, new("Pr2fV", 0)); // crumbler plat 2
            MapCrash2.AddVisual(26, 0 + 3000, new("Pr3fV")); // leaner
            MapCrash2.AddVisual(26, 0 + 4000, new("Pr4fV")); // spinner
            MapCrash2.AddVisual(26, 4, new("PlafV")); // pillar array
            MapCrash2.AddVisual(26, 4 + 0x100, new("Pb2mV")); // pillar array with possum
            MapCrash2.AddVisual(26, 7, new("Si0fV")); // easy/hard sign
            MapCrash2.AddVisual(27, 0, new("Po1eV")); // porcupine
            MapCrash2.AddVisual(28, 0, new("Ra1aV")); // rat
            MapCrash2.AddVisual(28, 2, new("Pb2cV")); // possum night jungle
            MapCrash2.AddVisual(28, 3, new("Lp1cV")); // lizard night jungle
            MapCrash2.AddVisual(28, 2 + 1000, new("Pb2mV")); // possum ruins
            MapCrash2.AddVisual(28, 3 + 1000, new("Lp1mV")); // lizard ruins
            MapCrash2.AddVisual(28, 4, new("Ra1aV")); // rat circle
            MapCrash2.AddVisual(30, 0, new("Ob2bV")); // ostrich
            MapCrash2.AddVisual(32, 0, new("Sm1eV")); // smasher
            MapCrash2.AddVisual(32, 1, new("Sm2eV")); // constant smasher
            MapCrash2.AddVisual(32, 2, new("Ro1eV")); // roller
            MapCrash2.AddVisual(32, 3, new("Ic3eV", 0)); // icicle
            MapCrash2.AddVisual(33, 0, new("AB10V")); // ass banger
            //MapCrash2.AddVisual(34, 0, new("BT10V")); // box tnt
            //MapCrash2.AddVisual(34, 2, new("BN10V")); // box empty
            //MapCrash2.AddVisual(34, 3, new("BS10V", 0)); // box spring
            //MapCrash2.AddVisual(34, 4, new("BC10V", 0)); // box continue
            //MapCrash2.AddVisual(34, 4 + 1000, new("BC1iV", 0)); // box continue (space level)
            //MapCrash2.AddVisual(34, 5, new("BI10V")); // box iron
            //MapCrash2.AddVisual(34, 6, new("BF10V", 0)); // box fruit
            //MapCrash2.AddVisual(34, 7, new("BA10V", 0)); // box action
            //MapCrash2.AddVisual(34, 8, new("BL10V")); // box life
            //MapCrash2.AddVisual(34, 9, new("BD10V")); // box doctor
            //MapCrash2.AddVisual(34, 10, new("Bp10V")); // box pickup
            //MapCrash2.AddVisual(34, 13, new("BG10V")); // box ghost
            //MapCrash2.AddVisual(34, 15, new("BS20V", 0)); // box iron spring
            //MapCrash2.AddVisual(34, 18, new("Bn10V")); // box nitro
            //MapCrash2.AddVisual(34, 19, new("BG10V")); // box ghost iron
            //MapCrash2.AddVisual(34, 23, new("Bs10V", 0)); // box steel
            //MapCrash2.AddVisual(34, 24, new("Ba10V", 0)); // box action nitro
            //if (Settings.Default.ShowCustomCrates)
            //{
            //    MapCrash2.AddVisual(34, 11, new("BP10V", 0)); // box pow
            //    MapCrash2.AddVisual(34, 12, new("Bp20V")); // box action purple
            //    MapCrash2.AddVisual(34, 17, new("Bl10V")); // box slot
            //    MapCrash2.AddVisual(34, 25, new("Bs20V", 0)); // box steel pickup
            //    MapCrash2.AddVisual(34, 26, new("BF20V", 0)); // box steel fruit
            //    MapCrash2.AddVisual(34, 27, new("Bc20V", 0)); // box iron continue
            //    MapCrash2.AddVisual(34, 28, new("BW10V", 0)); // box action switch off
            //    MapCrash2.AddVisual(34, 29, new("BW20V", 0)); // box action switch on
            //    MapCrash2.AddVisual(34, 30, new("Bw10V")); // box switch off (red)
            //    MapCrash2.AddVisual(34, 31, new("Bw30V")); // box switch on (green)
            //    MapCrash2.AddVisual(34, 32, new("Bw10V")); // box switch off (green)
            //    MapCrash2.AddVisual(34, 33, new("Bw20V")); // box switch on (red)
            //}
            MapCrash2.AddVisual(35, 1, new("Je1iV")); // space jetpack
            MapCrash2.AddVisual(35, 1 + 1000, new("Je2iV")); // space jetpack
            MapCrash2.AddVisual(35, 3, new("Do1iV", 0)); // space door
            MapCrash2.AddVisual(35, 6, new("Do2iV", 0)); // space lock
            MapCrash2.AddVisual(35, 6 + 1000, new("Do4iV")); // space lock (lamps)
            MapCrash2.AddVisual(35, 15, new("Sb1iV")); // space bomb ring
            MapCrash2.AddVisual(35, 11, new("Ca1iV")); // space cable
            MapCrash2.AddVisual(35, 13, new("SG60V", 0)); // space gun
            MapCrash2.AddVisual(36, 8, new("We1lV", 0)); // warp lift
            MapCrash2.AddVisual(38, 0, new("Ep1nV")); // spore plant
            MapCrash2.AddVisual(38, 0 + 1000, new("Ep5nV", 0)); // spore plant
            MapCrash2.AddVisual(38, 4, new("JuPpV")); // evil plant
            MapCrash2.AddVisual(39, 0, new("Mi1lV", 0)); // mine
            MapCrash2.AddVisual(39, 1, new("Fe1lV")); // fence
            MapCrash2.AddVisual(39, 2, new("pl1lV")); // plank
            MapCrash2.AddVisual(39, 4, new("Fe3lV", 6)); // timed spark long
            MapCrash2.AddVisual(39, 6, new("Fe4lV", 3)); // timed spark
            MapCrash2.AddVisual(39, 7, new("Ti1lV", 0)); // tiki
            MapCrash2.AddVisual(39, 9, new("Ft1lV")); // fence tile
            MapCrash2.AddVisual(41, 0, new("Bo1lV")); // boulder
            MapCrash2.AddVisual(42, 0, new("SL1iV")); // space lab ass
            MapCrash2.AddVisual(42, 2, new("SF1iV")); // space fire
            MapCrash2.AddVisual(45, 1, new("LJ1lV")); // labjack
            MapCrash2.AddVisual(46, 0, new("Dr1cV")); // dragonfly
            MapCrash2.AddVisual(46, 1, new("Dr1cV")); // dragonfly
            MapCrash2.AddVisual(47, 1, new("Hp1pV")); // hippo
            MapCrash2.AddVisual(47, 2, new("Bo1pV")); // board
            MapCrash2.AddVisual(47, 4, new("Mf1pV")); // mine float
            MapCrash2.AddVisual(47, 5, new("Pa1pV", 0)); // piranha fish
            MapCrash2.AddVisual(47, 6, new("Ra1kV")); // ramp
            MapCrash2.AddVisual(47, 7, new("Mf1pV")); // mine path
            MapCrash2.AddVisual(50, 8, new("Cl1sV")); // intro crystal light ray
            MapCrash2.AddVisual(50, 10, new("Sw1sV")); // intro star window
            MapCrash2.AddVisual(50, 15, new("Sb1sV")); // intro star window opacity 50%
            MapCrash2.AddVisual(50, 19, new("Li1sV")); // intro ship light
            MapCrash2.AddVisual(53, 0, new("Fr10V")); // fred
            MapCrash2.AddVisual(55, 0, new("Pu1gV")); // piston up
            MapCrash2.AddVisual(55, 1 + 0000, new("Pi2gV")); // piston small
            MapCrash2.AddVisual(55, 1 + 1000, new("Pi1gV")); // piston
            MapCrash2.AddVisual(55, 2, new("Pa1gV", 0)); // pad
            MapCrash2.AddVisual(55, 3, new("Gu1gV")); // gun
            MapCrash2.AddVisual(55, 4, new("Gd1gV")); // gun down
            MapCrash2.AddVisual(55, 11, new("Bp1gV")); // bonus plaque
            MapCrash2.AddVisual(55, 5, new("Rw1gV")); // robot walker
            MapCrash2.AddVisual(56, 0, new("AP1gV")); // ass pusher
            MapCrash2.AddVisual(57, 1, new("JuFcV")); // firefly
        }

        private const string MapsFileName = "CrashEdit.exe.entityvisuals-v1.json";

        public static void SaveMaps()
        {
            static void WriteMap(Utf8JsonWriter writer, EntityVisualList map, string name)
            {
                writer.WriteStartObject(name);
                writer.WriteStartArray("models");
                foreach (var kvp in map)
                {
                    writer.WriteStartObject();
                    writer.WriteNumber("type", kvp.Key / 100000);
                    writer.WriteNumber("subtype", kvp.Key % 100000);
                    writer.WriteString("anim", kvp.Value.AnimName);
                    writer.WriteNumber("frame", kvp.Value.AnimFrame);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            using var stream = new FileStream(MapsFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
            writer.WriteStartObject();
            WriteMap(writer, MapCrash1, "crash1");
            WriteMap(writer, MapCrash2, "crash2");
            writer.WriteEndObject();
            writer.Flush();
        }

        public static void LoadMaps()
        {
            static void ReadMap(EntityVisualList map, JsonProperty elt)
            {
                foreach (var vis in elt.Value.GetProperty("models").EnumerateArray())
                {
                    int type = vis.GetProperty("type").GetInt32();
                    int subtype = vis.GetProperty("subtype").GetInt32();
                    string anim = vis.GetProperty("anim").GetString()!;
                    int frame = vis.GetProperty("frame").GetInt32();
                    map.AddVisual(type, subtype, new(anim, frame));
                }
            }

            if (!File.Exists(MapsFileName))
                return;

            try
            {
                using var stream = File.OpenRead(MapsFileName);
                using var json = JsonDocument.Parse(stream);
                foreach (var elt in json.RootElement.EnumerateObject())
                {
                    if (elt.NameEquals("crash1"))
                    {
                        ReadMap(MapCrash1, elt);
                    }
                    else if (elt.NameEquals("crash2"))
                    {
                        ReadMap(MapCrash2, elt);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
