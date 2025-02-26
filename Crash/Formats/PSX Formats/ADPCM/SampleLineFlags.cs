namespace CrashEdit.Crash
{
    // 0x04, 0x06  Set the beginning of the current block to the loop address    
    // 0x03        Jump to the loop address after playing the current block
    // 0x07        The combination of the above two, play the current block endlessly
    // 0x01        Jump to the loop address but stop the envelope
    // 0x05        Play the current block endlessly but stop the envelope
    // 0x02        Don't forcibly stop the envelope

    [Flags]
    public enum SampleLineFlags : byte
    {
        None = 0,
        StopEnvelope = 1,
        NoForceStopEnvelope = 2,
        LoopEnd = 3,
        LoopStart = 4,
        LoopStartAlt = 6,
    }
}
