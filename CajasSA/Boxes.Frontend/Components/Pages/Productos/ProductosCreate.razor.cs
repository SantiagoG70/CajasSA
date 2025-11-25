using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages.Productos
{
    [Authorize(Roles = "Admin")]
    public partial class ProductosCreate
    {
        private ProductoDTO productDTO = new()
        {
            ProductCategoryIds = new List<int>(),
            ProductImages = new List<string>()
        };

        private EditContext editContext = null!;
        private ProductosForm? productForm;
        private List<Categoria> selectedCategories = new();
        private List<Categoria> nonSelectedCategories = new();
        private bool loading = true;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(productDTO);
            var httpActionResponse = await Repository.GetAsync<List<Categoria>>("/api/categorias/combo");
            loading = false;

            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR : {message}" , Severity.Error);
                return;
            }

            nonSelectedCategories = httpActionResponse.Response!;
        }

        private async Task CreateAsync()
        {
            productDTO.ProductCategoryIds = selectedCategories.Select(x => x.Id).ToList();

            var httpActionResponse = await Repository.PostAsync("/api/productos/full", productDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Return();
            Snackbar.Add("Registro creado", Severity.Success);
        }

        private void Return()
        {
            if (productForm != null)
            {
                productForm.FormPostedSuccessfully = true;
            }
            NavigationManager.NavigateTo($"/productos");
        }

    }
}
