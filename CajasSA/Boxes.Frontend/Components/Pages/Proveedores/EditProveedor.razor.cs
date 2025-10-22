using Microsoft.AspNetCore.Components;
using Boxes.Shared.Entites;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages.Proveedores
{
    public partial class EditProveedor
    {
        private Employee? employee;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<Employee>($"api/employees/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("employees");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
            }
            else
            {
                employee = responseHttp.Response;
                StateHasChanged();
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/employees", employee);

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
            NavigationManager.NavigateTo("employees");
        }
    }
}
