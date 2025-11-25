using Boxes.Frontend.Helpers;
using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Enums;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;

namespace Boxes.Frontend.Components.Pages.Cart
{
    public partial class DetallerOrden
    {
        private Orden? order;

        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private IConfirmDialogService confirmDialogService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int OrderId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHppt = await Repository.GetAsync<Orden>($"api/ordenes/{OrderId}");
            if (responseHppt.Error)
            {
                if (responseHppt.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/ordenes");
                    return;
                }
                var messageError = await responseHppt.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                return;
            }
            order = responseHppt.Response;
        }

        private async Task CancelOrderAsync()
        {
            await ModifyTemporalOrder("cancelar", OrdenStatus.Cancelled);
        }

        private async Task DispatchOrderAsync()
        {
            await ModifyTemporalOrder("despachar", OrdenStatus.Dispatched);
        }

        private async Task SendOrderAsync()
        {
            await ModifyTemporalOrder("enviar", OrdenStatus.Sent);
        }

        private async Task ConfirmOrderAsync()
        {
            await ModifyTemporalOrder("confirmar", OrdenStatus.Confirmed);
        }

        private async Task ModifyTemporalOrder(string message, OrdenStatus status)
        {
            var confirmResult = await confirmDialogService.ShowConfirmationAsync("Confirmacion", "Hay cambios sin guardar. ¿Desea salir de todas formas?");
            if (!confirmResult)
            {
                return;
            }

            var orderDTO = new OrdenDTO
            {
                Id = OrderId,
                OrdenStatus = status
            };

            var responseHttp = await Repository.PutAsync("api/ordenes", orderDTO);
            if (responseHttp.Error)
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }
            NavigationManager.NavigateTo("/ordenes");
        }
    }
}
