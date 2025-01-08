using CrashEdit.Crash.GOOLIns;

namespace CrashEdit.Crash
{
    public enum GOOLVersion
    {
        Version0, // 1995 + Proto
        Version1, // Crash 1
        Version2, // Crash 2
        Version3  // Crash 3
    }

    public enum ObjectFields
    {
        self = 0,
        parent = 1,
        brother = 2,
        child = 3,
        creator = 4,
        player = 5,
        collider = 6,
        interrupter = 7,
        x = 8,
        y = 9,
        z = 10,
        rotx = 11,
        roty = 12,
        rotz = 13,
        scax = 14,
        scay = 15,
        scaz = 16,
        velx = 17,
        vely = 18,
        velz = 19,
        trotx = 20,
        troty = 21,
        trotz = 22,
        vecx = 23,
        vecy = 24,
        vecz = 25,
        statusa = 26,
        statusb = 27,
        blockflag = 28,
        subtype = 29,
        id = 30,
        sp = 31,
        pc = 32,
        fp = 33,
        tpc = 34,
        epc = 35,
        hpc = 36,
        misc = 37,
        var = 38,
        frametime = 39,
        statetime = 40,
        stalltime = 41,
        framegroup = 42,
        framenum = 43,
        entity = 44,
        pathprog = 45,
        pathlen = 46,
        groundy = 47,
        stateflag = 48,
        speed = 49,
        displaymode = 50,
        field_51 = 51,
        groundtime = 52,
        groundvel = 53,
        zindex = 54,
        eventreceived = 55,
        vfx = 56,
        yzapproach = 57,
        density = 58,
        voice = 59,
        field_60 = 60,
        field_61 = 61,
        field_62 = 62,
        field_63 = 63,
        mem1 = 64,
        mem2, mem3, mem4, mem5, mem6, mem7, mem8, mem9,
        mem10, mem11, mem12, mem13, mem14, mem15, mem16, mem17, mem18, mem19,
        mem20, mem21, mem22, mem23, mem24, mem25, mem26, mem27, mem28, mem29,
        mem30, mem31, mem32, mem33, mem34, mem35, mem36, mem37, mem38, mem39,
        mem40, mem41, mem42, mem43, mem44, mem45, mem46, mem47, mem48, mem49,
        mem50, mem51, mem52, mem53, mem54, mem55, mem56, mem57, mem58, mem59,
        mem60, mem61, mem62, mem63
    };

    public enum ObjectColors1
    {
        lightmat11 = 0,
        lightmat12,
        lightmat13,
        lightmat21,
        lightmat22,
        lightmat23,
        lightmat31,
        lightmat32,
        lightmat33,
        backr,
        backg,
        backb,
        colormatr1,
        colormatg1,
        colormatb1,
        colormatr2,
        colormatg2,
        colormatb2,
        colormatr3,
        colormatg3,
        colormatb3,
        intr,
        intg,
        intb
    }

    public enum ObjectColors2
    {
        mod1 = 0,
        mod2,
        mod3,
        modfinal,
        colr1,
        colg1,
        colb1,
        colr2,
        colg2,
        colb2,
        colr3,
        colg3,
        colb3,
        finalr,
        finalg,
        finalb
    }

    public enum ControllerButtons
    {
        L2 = 0x0001,
        R2 = 0x0002,
        L1 = 0x0004,
        R1 = 0x0008,
        Triangle = 0x0010,
        Circle = 0x0020,
        X = 0x0040,
        Square = 0x0080,
        Select = 0x0100,
        L3 = 0x0200,
        R3 = 0x0400,
        Start = 0x0800,
        Up = 0x1000,
        Right = 0x2000,
        Down = 0x4000,
        Left = 0x8000
    }

    public static class GOOLInterpreter
    {
        private static string[] GlobalsCrash1 = new string[512];
        private static string[] GlobalsCrash2 = new string[512];
        private static string[] GlobalsCrash3 = new string[512];

        static GOOLInterpreter()
        {
            GlobalsCrash1[0x0] = "<LEVEL>";
            GlobalsCrash1[0x1] = "<GLOBALVAL>";
            GlobalsCrash1[0x2] = "<SHAKEY>";
            GlobalsCrash1[0x3] = "<GLOBALOBJ>";
            GlobalsCrash1[0x4] = "<GAMEFLAGS>";
            GlobalsCrash1[0x5] = "<RESPAWNCOUNT>";
            GlobalsCrash1[0x6] = "<FRUITDISPLAY>";
            GlobalsCrash1[0x7] = "<LIFEDISPLAY>";
            GlobalsCrash1[0x9] = "<PREVGAMEFLAGS>";
            GlobalsCrash1[0xC] = "<PAUSEMENU>";
            GlobalsCrash1[0xD] = "<LIFEICONTRANSX>";
            GlobalsCrash1[0xE] = "<PICKUPDISPLAY>";
            GlobalsCrash1[0xF] = "<GAMEDIR>";
            GlobalsCrash1[0x10] = "<DOCTOR>";
            GlobalsCrash1[0x14] = "<CURRENTLEVEL>";
            GlobalsCrash1[0x18] = "<LIFECOUNT>";
            GlobalsCrash1[0x19] = "<HEALTH>";
            GlobalsCrash1[0x1A] = "<FRUITCOUNT>";
            GlobalsCrash1[0x1E] = "<ZONEFLAGS>";
            GlobalsCrash1[0x1F] = "<STARTLIVES>";
            GlobalsCrash1[0x21] = "<MONOSOUND>";
            GlobalsCrash1[0x22] = "<SFXVOL>";
            GlobalsCrash1[0x23] = "<MUSVOL>";
            GlobalsCrash1[0x24] = "<CAMSPINOBJ>";
            GlobalsCrash1[0x25] = "<CAMTRANSX>";
            GlobalsCrash1[0x26] = "<CAMTRANSY>";
            GlobalsCrash1[0x27] = "<CAMTRANSZ>";
            GlobalsCrash1[0x28] = "<CAMROTX>";
            GlobalsCrash1[0x29] = "<CAMROTY>";
            GlobalsCrash1[0x2A] = "<CAMROTZ>";
            GlobalsCrash1[0x2B] = "<FRAMETIME>";
            GlobalsCrash1[0x44] = "<DEBUG>";
            GlobalsCrash1[0x45] = "<SCREENOFFX>";
            GlobalsCrash1[0x46] = "<SCREENOFFY>";
            GlobalsCrash1[0x47] = "<LEVELCOUNT>";
            GlobalsCrash1[0x48] = "<LEVELSUNLOCKED>";
            GlobalsCrash1[0x49] = "<CAMSPINOBJVERT>";
            GlobalsCrash1[0x4E] = "<LIGHTSRCOBJ>";
            GlobalsCrash1[0x56] = "<CAMSPINDISTSPD>";
            GlobalsCrash1[0x57] = "<CAMSPINLOOKSPD>";
            GlobalsCrash1[0x58] = "<PERCENTCOMPLETE>";
            GlobalsCrash1[0x59] = "<CARDSTATUS>";
            GlobalsCrash1[0x5A] = "<BONUSROUND>";
            GlobalsCrash1[0x5B] = "<CARDBLOCKCOUNT>";
            GlobalsCrash1[0x5C] = "<BOXCOUNT>";
            GlobalsCrash1[0x5D] = "<ITEMPOOL1>";
            GlobalsCrash1[0x5E] = "<ISLANDCAMANGLE>";
            GlobalsCrash1[0x5F] = "<GEMTIME>";
            GlobalsCrash1[0x60] = "<ISLANDCAMSTATUS>";
            GlobalsCrash1[0x61] = "<FIRSTZONE>";
            GlobalsCrash1[0x62] = "<DEBUG>";
            GlobalsCrash1[0x63] = "<CHECKPOINTID>";
            GlobalsCrash1[0x64] = "<PREVBOXCOUNT>";
            GlobalsCrash1[0x65] = "<PREVLEVEL>";
            GlobalsCrash1[0x66] = "<ITEMPOOL2>";
            GlobalsCrash1[0x67] = "<MAPLEVELLINKS>";
            GlobalsCrash1[0x68] = "<TITLEPAUSESTATE>";
            GlobalsCrash1[0x69] = "<MAPKEYLINKS>";
            GlobalsCrash1[0x6A] = "<ISLANDTEXTOBJ>";
            GlobalsCrash1[0x6B] = "<ISLANDTEXTANIM>";
            GlobalsCrash1[0x6C] = "<ISLANDTEXTFRAME>";
            GlobalsCrash1[0x6D] = "<GAMETICK>";
            GlobalsCrash1[0x6E] = "<CARDTEXTBUF>";
            GlobalsCrash1[0x6F] = "<CARDICONBUF>";
            GlobalsCrash1[0x70] = "<CARDBLOCKDATA00>";
            GlobalsCrash1[0x71] = "<CARDBLOCKDATA01>";
            GlobalsCrash1[0x72] = "<CARDBLOCKDATA02>";
            GlobalsCrash1[0x73] = "<CARDBLOCKDATA03>";
            GlobalsCrash1[0x74] = "<CARDBLOCKDATA04>";
            GlobalsCrash1[0x75] = "<CARDBLOCKDATA05>";
            GlobalsCrash1[0x76] = "<CARDBLOCKDATA06>";
            GlobalsCrash1[0x77] = "<CARDBLOCKDATA07>";
            GlobalsCrash1[0x78] = "<CARDBLOCKDATA08>";
            GlobalsCrash1[0x79] = "<CARDBLOCKDATA09>";
            GlobalsCrash1[0x7A] = "<CARDBLOCKDATA10>";
            GlobalsCrash1[0x7B] = "<CARDBLOCKDATA11>";
            GlobalsCrash1[0x7C] = "<CARDBLOCKDATA12>";
            GlobalsCrash1[0x7D] = "<CARDBLOCKDATA13>";
            GlobalsCrash1[0x7E] = "<CARDBLOCKDATA14>";
            GlobalsCrash1[0x7F] = "<GEMCOUNT>";
            GlobalsCrash1[0x80] = "<KEYCOUNT>";
            GlobalsCrash1[0x81] = "<SAVETYPE>";
            GlobalsCrash1[0x82] = "<SAVEDITEMPOOL1>";
            GlobalsCrash1[0x83] = "<SAVEDITEMPOOL2>";
            GlobalsCrash1[0x84] = "<SPAWNTRANSX>";
            GlobalsCrash1[0x85] = "<SPAWNTRANSY>";
            GlobalsCrash1[0x86] = "<SPAWNTRANSZ>";
            GlobalsCrash1[0x88] = "<FADECONTROL>";
            GlobalsCrash1[0x89] = "<FADEAMOUNT>";
            GlobalsCrash1[0x8A] = "<DEATHCOUNT>";
            GlobalsCrash1[0x8C] = "<AUTOPASSWORD>";
            GlobalsCrash1[0x8D] = "<AUTOPASSWORDINPUT1>";
            GlobalsCrash1[0x8E] = "<AUTOPASSWORDINPUT2>";
            GlobalsCrash1[0x8F] = "<SAVEDLEVELCOUNT>";
            GlobalsCrash1[0x90] = "<DEMOID>";
            GlobalsCrash1[0x91] = "<OPTIONSCHANGED>";
            GlobalsCrash1[0x92] = "<DOCTORHELPCOUNT>";
            GlobalsCrash1[0x93] = "<CARDBIOSTEXTBUF>";

            GlobalsCrash2[0x0] = GlobalsCrash1[0x0];
            GlobalsCrash2[0x1] = GlobalsCrash1[0x1];
            GlobalsCrash2[0x2] = GlobalsCrash1[0x2];
            GlobalsCrash2[0x3] = GlobalsCrash1[0x3];
            GlobalsCrash2[0x4] = GlobalsCrash1[0x4];
            GlobalsCrash2[0x5] = GlobalsCrash1[0x5];
            GlobalsCrash2[0x6] = GlobalsCrash1[0x6];
            GlobalsCrash2[0x7] = GlobalsCrash1[0x7];
            GlobalsCrash2[0x9] = GlobalsCrash1[0x9];
            GlobalsCrash2[0xC] = GlobalsCrash1[0xC];
            GlobalsCrash2[0xD] = GlobalsCrash1[0xD];
            GlobalsCrash2[0xE] = GlobalsCrash1[0xE];
            GlobalsCrash2[0xF] = GlobalsCrash1[0xF];
            GlobalsCrash2[0x10] = GlobalsCrash1[0x10];
            GlobalsCrash2[0x14] = GlobalsCrash1[0x14];
            GlobalsCrash2[0x16] = "<BONUSDEATHTIME>";
            GlobalsCrash2[0x18] = GlobalsCrash1[0x18];
            GlobalsCrash2[0x19] = GlobalsCrash1[0x19];
            GlobalsCrash2[0x1A] = GlobalsCrash1[0x1A];
            GlobalsCrash2[0x1D] = "<BOXCOUNTER>";
            GlobalsCrash2[0x1E] = GlobalsCrash1[0x1E];
            GlobalsCrash2[0x1F] = GlobalsCrash1[0x1F];
            GlobalsCrash2[0x21] = GlobalsCrash1[0x21];
            GlobalsCrash2[0x22] = GlobalsCrash1[0x22];
            GlobalsCrash2[0x23] = GlobalsCrash1[0x23];
            GlobalsCrash2[0x24] = "<CHECKPOINTCOUNT>";
            GlobalsCrash2[0x25] = GlobalsCrash1[0x25];
            GlobalsCrash2[0x26] = GlobalsCrash1[0x26];
            GlobalsCrash2[0x27] = GlobalsCrash1[0x27];
            GlobalsCrash2[0x28] = GlobalsCrash1[0x28];
            GlobalsCrash2[0x29] = GlobalsCrash1[0x29];
            GlobalsCrash2[0x2A] = GlobalsCrash1[0x2A];
            GlobalsCrash2[0x2B] = GlobalsCrash1[0x2B];
            GlobalsCrash2[0x2E] = "<SAVEDGEMPOOL1>";
            GlobalsCrash2[0x2F] = "<SAVEDGEMPOOL2>";
            GlobalsCrash2[0x30] = "<GEMPOOL1>";
            GlobalsCrash2[0x31] = "<GEMPOOL2>";
            GlobalsCrash2[0x38] = "<CRYSTALPOOL1>";
            GlobalsCrash2[0x39] = "<CRYSTALPOOL2>";
            GlobalsCrash2[0x3E] = "<BOXCOUNT>";
            GlobalsCrash2[0x44] = GlobalsCrash1[0x44];
            GlobalsCrash2[0x45] = "<CHECKPOINTID>";
            GlobalsCrash2[0x46] = "<PREVBOXCOUNT>";
            GlobalsCrash2[0x47] = "<PREVLEVEL>";
            GlobalsCrash2[0x4C] = "<DEMOTEXTOBJ>";
            GlobalsCrash2[0x4F] = "<GAMETICK>";
            GlobalsCrash2[0x66] = "<SPAWNTRANSX>";
            GlobalsCrash2[0x67] = "<SPAWNTRANSY>";
            GlobalsCrash2[0x68] = "<SPAWNTRANSZ>";
            GlobalsCrash2[0x6A] = "<FADECONTROL>";
            GlobalsCrash2[0x6B] = "<FADEAMOUNT>";
            GlobalsCrash2[0x6C] = "<DEATHCOUNT>";
            GlobalsCrash2[0x7C] = "<ANALOGAMT>";
            GlobalsCrash2[0x7D] = "<ANALOGDIR>";
            GlobalsCrash2[0x84] = "<BONUSSTATE>";
            GlobalsCrash2[0x85] = "<BONUSFRUITCOUNTER>";
            GlobalsCrash2[0x86] = "<BONUSLIFECOUNTER>";
            GlobalsCrash2[0x87] = "<BONUSBOXCOUNTER>";
            GlobalsCrash2[0x8D] = "<SAVEDCRYSTALPOOL1>";
            GlobalsCrash2[0x8E] = "<SAVEDCRYSTALPOOL2>";
            GlobalsCrash2[0x91] = "<SECTION>";
            GlobalsCrash2[0x92] = "<SECTIONDEATHCOUNT>";
            GlobalsCrash2[0x93] = "<LEVELBOXCOUNT>";

            GlobalsCrash3[0x0] = GlobalsCrash2[0x0];
            GlobalsCrash3[0x44] = GlobalsCrash2[0x44];
        }

        public static string GetGlobalName(GOOLVersion version, int index)
        {
            var globals = GlobalsCrash1;
            switch (version)
            {
                case GOOLVersion.Version2: globals = GlobalsCrash2; break;
                case GOOLVersion.Version3: globals = GlobalsCrash3; break;
            }

            if (index < 0 || index >= globals.Length)
                return null;
            return globals[index];
        }

        public static GOOLVersion GetVersion(GameVersion ver)
        {
            switch (ver)
            {
                case GameVersion.Crash1Beta1995:
                case GameVersion.Crash1BetaMAR08:
                    return GOOLVersion.Version0;
                case GameVersion.Crash1:
                case GameVersion.Crash1BetaMAY11:
                default:
                    return GOOLVersion.Version1;
                case GameVersion.Crash2:
                    return GOOLVersion.Version2;
                case GameVersion.Crash3:
                    return GOOLVersion.Version3;
            }
        }

        public static int GetProcessOff(GameVersion ver) => GetProcessOff(GetVersion(ver));

        public static int GetProcessOff(GOOLVersion ver)
        {
            switch (ver)
            {
                default:
                case GOOLVersion.Version0:
                case GOOLVersion.Version1:
                    return 0x60;
                case GOOLVersion.Version2:
                case GOOLVersion.Version3:
                    return 0x40;
            }
        }

        public static bool IsReturnInstruction(GOOLInstruction ins)
        {
            switch (ins.GOOL.Version)
            {
                case GOOLVersion.Version0:
                    return ins.Type == typeof(Cfl_95) && ins.Args['T'].Value == 2;
                case GOOLVersion.Version1:
                    return ins.Type == typeof(Cfl) && ins.Args['T'].Value == 2;
                case GOOLVersion.Version2:
                case GOOLVersion.Version3:
                    return ins.Type == typeof(Ret);
                default:
                    return false;
            }
        }

        public static bool IsMIPSInstruction(GOOLInstruction ins)
        {
            switch (ins.GOOL.Version)
            {
                case GOOLVersion.Version2:
                case GOOLVersion.Version3:
                    return ins.Type == typeof(Mips);
                default:
                    return false;
            }
        }

        public static string GetColor(GOOLVersion ver, int col)
        {
            switch (ver)
            {
                case GOOLVersion.Version0:
                case GOOLVersion.Version1:
                    return ((ObjectColors1)col).ToString();
                case GOOLVersion.Version2:
                case GOOLVersion.Version3:
                    return ((ObjectColors2)col).ToString();
                default:
                    return col.ToString();
            }
        }
    }
}
