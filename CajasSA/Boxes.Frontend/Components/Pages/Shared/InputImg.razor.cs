using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Boxes.Frontend.Components.Pages.Shared
{
    public partial class InputImg
    {
        private string? imageBase64;

        [Parameter] public string Label { get; set; } = "Imagen";
        [Parameter] public string? ImageURL { get; set; }
        [Parameter] public EventCallback<string> ImageSelected { get; set; }

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            var imagenes = e.GetMultipleFiles();

            foreach (var imagen in imagenes)
            {
                var arrBytes = new byte[imagen.Size];
                await imagen.OpenReadStream().ReadExactlyAsync(arrBytes);
                imageBase64 = Convert.ToBase64String(arrBytes);
                ImageURL = null;
                await ImageSelected.InvokeAsync(imageBase64);
                StateHasChanged();
            }
        }
    }
}
