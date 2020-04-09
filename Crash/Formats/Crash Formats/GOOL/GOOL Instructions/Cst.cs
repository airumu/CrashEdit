﻿namespace Crash.GOOLIns
{
    [GOOLInstruction(53,GameVersion.Crash2)]
    [GOOLInstruction(53,GameVersion.Crash3)]
    public sealed class Cst : GOOLInstruction
    {
        public Cst(int value,GOOLEntry gool) : base(value,gool) { }

        public override string Name => "CST";
        public override string Format => "SSSSSSSSSSSSSS (RRRRRR) 00 10";
        public override string Comment => $"change to state {GetArg('S')}";
    }
}
