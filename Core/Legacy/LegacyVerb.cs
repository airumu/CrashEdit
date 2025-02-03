namespace CrashEdit
{

    public sealed class LegacyVerb : Verb
    {

        public LegacyVerb(string text, Action proc)
        {
            ArgumentNullException.ThrowIfNull(text);
            ArgumentNullException.ThrowIfNull(proc);

            _text = text;
            Proc = proc;
        }

        public LegacyVerb(string text, string imageKey, Action proc)
        {
            ArgumentNullException.ThrowIfNull(text);
            ArgumentNullException.ThrowIfNull(imageKey);
            ArgumentNullException.ThrowIfNull(proc);

            _text = text;
            _imageKey = imageKey;
            Proc = proc;
        }

        public string _text;
        public string _imageKey;

        public override string Text => _text;
        public override string ImageKey => _imageKey;
        private Action Proc { get; }

        public override void Execute(IUserInterface ui)
        {
            ArgumentNullException.ThrowIfNull(ui);

            Proc();
        }

    }

}
