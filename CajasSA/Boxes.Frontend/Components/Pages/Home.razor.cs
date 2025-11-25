using Blazored.Modal.Services;
using Boxes.Frontend.Components.Pages.Auth;
using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages
{
    public partial class Home
    {
        private int currentPage = 1;
        private int totalPages;
        private int counter = 0;
        private bool isAuthenticated;
        private string allCategories = "all_categories_list";

        public List<Producto>? Products { get; set; }
        public List<Categoria>? Categories { get; set; }
        public string CategoryFilter { get; set; } = string.Empty;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 8;
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = null!;
        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        protected override async Task OnParametersSetAsync()
        {
            await CheckIsAuthenticatedAsync();
            await LoadCounterAsync();
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Categoria>>("api/categorias/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            }

            Categories = responseHttp.Response;
        }

        private async Task CheckIsAuthenticatedAsync()
        {
            var authenticationState = await authenticationStateTask;
            isAuthenticated = authenticationState.User.Identity!.IsAuthenticated;
        }

        private async Task LoadCounterAsync()
        {
            if (!isAuthenticated)
            {
                return;
            }

            var responseHttp = await Repository.GetAsync<int>("/api/OrdenesTemporales/count");
            if (responseHttp.Error)
            {
                return;
            }
            counter = responseHttp.Response;
        }

        private async Task AddToCartAsync(int productId)
        {
            if (!isAuthenticated)
            {
                Modal.Show<Login>();
                Snackbar.Add("Debes haber iniciado sesión para poder agregar productos al carro de compras.", Severity.Error);
                return;
            }

            var temporalOrderDTO = new OrdenTemporalDTO
            {
                ProductId = productId
            };

            var httpActionResponse = await Repository.PostAsync("/api/OrdenesTemporales/full", temporalOrderDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"Error: {message}", Severity.Error);
                return;
            }

            await LoadCounterAsync();

            Snackbar.Add("Producto agregado al carro de compras.", Severity.Success);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task SelectedRecordsNumberAsync(int recordsnumber)
        {
            RecordsNumber = recordsnumber;
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }

        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task LoadAsync(int page = 1, string category = "")
        {
            if (!string.IsNullOrWhiteSpace(category))
            {
                if (category == allCategories)
                {
                    CategoryFilter = string.Empty;
                }
                else
                {
                    CategoryFilter = category;
                }
            }
            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }

            var ok = await LoadListAsync(page);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private void ValidateRecordsNumber(int recordsnumber)
        {
            if (recordsnumber == 0)
            {
                RecordsNumber = 8;
            }
        }

        private async Task<bool> LoadListAsync(int page)
        {
            ValidateRecordsNumber(RecordsNumber);
            var url = $"api/productos?page={page}&RecordsNumber={RecordsNumber}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }
            if (!string.IsNullOrEmpty(CategoryFilter))
            {
                url += $"&CategoryFilter={CategoryFilter}";
            }
            var response = await Repository.GetAsync<List<Producto>>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Products = response.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            ValidateRecordsNumber(RecordsNumber);
            var url = $"api/productos/totalPages/?RecordsNumber={RecordsNumber}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }
            if (!string.IsNullOrEmpty(CategoryFilter))
            {
                url += $"&CategoryFilter={CategoryFilter}";
            }

            var response = await Repository.GetAsync<int>(url);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = response.Response;
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }
    }
}