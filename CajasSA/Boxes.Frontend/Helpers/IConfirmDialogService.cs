namespace Boxes.Frontend.Helpers
{
    public interface IConfirmDialogService
    {
        Task<bool> ShowConfirmationAsync(string title, string message, string icon = "Warning");
    }
}
