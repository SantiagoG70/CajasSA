using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages.Cart
{
    public partial class ModifyOrdenTemporal
    {
        private List<string>? categories;
        private List<string>? images;
        private bool loading = true;
        private Producto? product;
        private OrdenTemporalDTO? temporalOrderDTO;

        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int TemporalOrderId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadTemporalOrderAsync();
        }

        private async Task LoadTemporalOrderAsync()
        {
            loading = true;
            var httpResponse = await Repository.GetAsync<OrdenTemporal>($"/api/OrdenesTemporales/{TemporalOrderId}");

            if (httpResponse.Error)
            {
                loading = false;
                var message = await httpResponse.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            var temporalOrder = httpResponse.Response!;
            temporalOrderDTO = new OrdenTemporalDTO
            {
                Id = temporalOrder.Id,
                ProductId = temporalOrder.ProductId,
                Remarks = temporalOrder.Remarks!,
                Quantity = temporalOrder.Quantity
            };
            product = temporalOrder.Product;
            categories = product!.ProductCategories!.Select(x => x.Category.Name).ToList();
            images = product.ProductImages!.Select(x => x.Image).ToList();
            loading = false;
        }

        public async Task UpdateCartAsync()
        {
            var httpResponse = await Repository.PutAsync("/api/OrdenesTemporales/full", temporalOrderDTO);
            if (httpResponse.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Snackbar.Add("El producto se ha actualizado correctamente en el carrito.", Severity.Success);
            NavigationManager.NavigateTo("/");
        }

    }
}
