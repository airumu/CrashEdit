namespace CrashEdit
{

    public sealed class FindPreviousCommand : Command
    {

        public FindPreviousCommand(ICommandHost host) : base(host) { }

        public override string Text => "Find Previous";

        public override string ImageKey => "Previous";

        public override bool Ready =>
            WsHost?.SearchPredicate != null &&
            WsHost?.ActiveController != null;

        public override bool Execute()
        {
            if (WsHost == null)
                throw new InvalidOperationException();
            if (WsHost.SearchPredicate == null)
                throw new InvalidOperationException();
            if (WsHost.ActiveController == null)
                throw new InvalidOperationException();

            // Start from the currently selected controller.
            var w = new Walker();
            w.Cursor = WsHost.ActiveController;

            // Regress until a match is found. The initial selection is not eligible.
            while (w.MoveToPreviousDFS())
            {
                if (WsHost.SearchPredicate(w.Cursor))
                {
                    // Match found.
                    WsHost.ActiveController = w.Cursor;
                    return true;
                }
            }

            // No match.
            Host.ShowInformation("No results before the current selection.", "Find");
            return false;
        }

    }

}
