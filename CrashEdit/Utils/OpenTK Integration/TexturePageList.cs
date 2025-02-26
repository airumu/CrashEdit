namespace CrashEdit.CE
{
    public sealed class TexturePageList : Dictionary<int, short>
    {
        public void AddTexturePage(int eid)
        {
            if (!ContainsKey(eid))
                this[eid] = (short)Count;
        }
    }
}
