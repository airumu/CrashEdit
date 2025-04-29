namespace CrashEdit.Crash
{
    [ChunkType(1)]
    public sealed class TextureChunkLoader : ChunkLoader
    {
        public override Chunk Load(byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);
            BitConv.ToInt32(data, 12, Chunk.CalculateChecksum(data));
            return new TextureChunk(data);
        }
    }
}
