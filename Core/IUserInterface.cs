namespace CrashEdit
{

    public interface IUserInterface
    {

        void ShowError(string msg);

        void ShowInformation(string msg, string title);

        bool ShowImportDialog(out string? filename, string[] fileFilters);

        bool ShowExportDialog(out string? filename, string[] fileFilters);

        UserChoice? ShowChoiceDialog(string msg, IEnumerable<UserChoice> choices);

    }

}
