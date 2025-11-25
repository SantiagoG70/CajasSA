using Boxes.Frontend.Helpers;
using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages.Cart
{
    public partial class ShowCart
    {
        public List<OrdenTemporal>? temporalOrders { get; set; }
        private float sumQuantity;
        private decimal sumValue;

        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private IJSRuntime JS { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private InvoiceService InvoiceService { get; set; } = null!;
        [Inject] private IConfirmDialogService confirmDialogService { get; set; } = null!;

        public OrdenDTO OrdenDTO { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
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
            var confirmResult = await confirmDialogService.ShowConfirmationAsync("Confirmacion", "¿Desea confirmar la orden?");
            if (!confirmResult)
            {
                return;
            }

            var httpActionResponse = await Repository.PostAsync("/api/ordenes", OrdenDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR : {message}", Severity.Error);
                return;
            }

            NavigationManager.NavigateTo("/Cart/OrdenConfirmed");
        }

        private async Task Delete(int temporalOrderId)
        {
            var confirmResult = await confirmDialogService.ShowConfirmationAsync("Confirmacion", "Hay cambios sin guardar. ¿Desea salir de todas formas?");
            if (!confirmResult)
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
                Snackbar.Add($"ERROR: {mensajeError}", Severity.Error);
                return;
            }

            await LoadAsync();
            var messageSuccess = "Registro eliminado";
            Snackbar.Add(messageSuccess, Severity.Success);
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