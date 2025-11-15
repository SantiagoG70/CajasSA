using Microsoft.AspNetCore.Components;
using Boxes.Shared.Entites;
using MudBlazor;
using Boxes.Frontend.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Boxes.Frontend.Components.Pages.Proveedores
{
    [Authorize(Roles = "Admin")]
    public partial class EditProveedor
    {
        private Proveedor? proveedor;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<Proveedor>($"api/proveedores/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("proveedores");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
            }
            else
            {
                proveedor = responseHttp.Response;
                StateHasChanged();
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/proveedores", proveedor);

            if (responseHttp.Error)
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
                return;
            }

            Return();
            Snackbar.Add("Registro guardado.", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("proveedores");
        }
    }
}
