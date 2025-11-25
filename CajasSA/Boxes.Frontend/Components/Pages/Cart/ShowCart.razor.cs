using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Boxes.Frontend.Components.Pages.Cart
{
    public partial class ShowCart
    {
        public List<OrdenTemporal>? temporalOrders { get; set; }
        private float sumQuantity;
        private decimal sumValue;

        [Inject] private IJSRuntime JS { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private InvoiceService InvoiceService { get; set; } = null!;

        public OrdenDTO OrdenDTO { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadAsync();
                StateHasChanged();
            }
        }

        private async Task LoadAsync()
        {
            try
            {
                var responseHppt = await Repository.GetAsync<List<OrdenTemporal>>("api/OrdenesTemporales/my");
                if (responseHppt.Error || responseHppt.Response is null)
                {
                    temporalOrders = new List<OrdenTemporal>();
                    sumQuantity = 0;
                    sumValue = 0;
                    return;
                }

                temporalOrders = responseHppt.Response;
                sumQuantity = temporalOrders.Sum(x => x.Quantity);
                sumValue = temporalOrders.Sum(x => x.Value);
            }
            catch
            {
                temporalOrders = new List<OrdenTemporal>();
                sumQuantity = 0;
                sumValue = 0;
            }
        }

        private async Task ConfirmOrderAsync()
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Esta seguro que quieres confirmar el pedido?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var httpActionResponse = await Repository.PostAsync("/api/ordenes", OrdenDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            NavigationManager.NavigateTo("/Cart/OrdenConfirmed");
        }

        private async Task Delete(int temporalOrderId)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Esta seguro que quieres borrar el registro?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync($"api/OrdenesTemporales/{temporalOrderId}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }

                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = false,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Producto eliminado del carro de compras.");
        }

        private async Task GenerateInvoiceAsync()
        {
            var products = temporalOrders?.Select(x => x.Product).ToList() ?? new List<Producto>();
            var total = sumValue;

            var pdfBytes = InvoiceService.GenerateInvoice(products, total);
            var base64 = Convert.ToBase64String(pdfBytes);

            await JS.InvokeVoidAsync("downloadPdf", base64, "Factura_Carrito.pdf");
        }
    }
}