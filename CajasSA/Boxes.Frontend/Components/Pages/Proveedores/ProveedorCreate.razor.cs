using Microsoft.AspNetCore.Components;
using Boxes.Shared.Entites;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages.Proveedores
{
    public partial class ProveedorCreate
    {
        private Proveedor employee = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/employees", employee);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Return();
            Snackbar.Add("Registro creado", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/employees");
        }
    }
}
