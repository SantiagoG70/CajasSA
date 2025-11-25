namespace Boxes.Frontend.Helpers
{
    public class ConfirmDialogService: IConfirmDialogService
    {
        
        public event Func<string, string, string, Task<bool>>? OnShow;

        
        public async Task<bool> ShowConfirmationAsync(string title, string message, string icon = "Warning")
        {
            
            if (OnShow is null)
            {
                
                return true;
            }

            
            return await OnShow.Invoke(title, message, icon);
        }
    }
}
