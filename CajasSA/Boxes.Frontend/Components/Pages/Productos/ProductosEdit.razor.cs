using Boxes.Frontend.Repositories;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Boxes.Frontend.Components.Pages.Productos
{
    [Authorize(Roles = "Admin")]
    public partial class ProductosEdit
    {
        private ProductoDTO productDTO = new()
        {
            ProductCategoryIds = new List<int>(),
            ProductImages = new List<string>()
        };

        private ProductosForm? productForm;
        private List<Categoria> selectedCategories = new();
        private List<Categoria> nonSelectedCategories = new();
        private bool loading = true;
        private Producto? producto;
        [Parameter] public int ProductId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProductAsync();
            await LoadCategoriesAsync();
        }

        private async Task AddImageAsync()
        {
            if (productDTO.ProductImages is null || productDTO.ProductImages.Count == 0)
            {
                return;
            }

            var imageDTO = new ImageDTO
            {
                ProductId = ProductId,
                Images = productDTO.ProductImages!
            };

            var httpActionResponse = await Repository.PostAsync<ImageDTO, ImageDTO>("/api/productos/addImages", imageDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR: {message}", Severity.Error);
                return;
            }

            productDTO.ProductImages = httpActionResponse.Response!.Images;
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Imagenes agregadas con éxito.");

        }

        private async Task RemoveImageAsyc()
        {
            if (productDTO.ProductImages is null || productDTO.ProductImages.Count == 0)
            {
                return;
            }

            var imageDTO = new ImageDTO
            {
                ProductId = ProductId,
                Images = productDTO.ProductImages!
            };

            var httpActionResponse = await Repository.PostAsync<ImageDTO, ImageDTO>("/api/productos/removeLastImage", imageDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR: {message}", Severity.Error);
                return;
            }

            productDTO.ProductImages = httpActionResponse.Response!.Images;
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Imagen eliminada con éxito.");

        }

        private async Task LoadProductAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<Producto>($"/api/productos/{ProductId}");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR: { message}", Severity.Error);
                return;
            }

            producto = httpActionResponse.Response!;
            productDTO = ToProductDTO(producto);
            loading = false;
        }

        private ProductoDTO ToProductDTO(Producto product)
        {
            return new ProductoDTO
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Type = product.Type,
                Quantity = product.Quantity,
                ProductCategoryIds = product.ProductCategories!.Select(x => x.CategoryId).ToList(),
                ProductImages = product.ProductImages!.Select(x => x.Image).ToList()
            };
        }

        private async Task LoadCategoriesAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<List<Categoria>>("/api/categorias/combo");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR: { message}", Severity.Error);
                return;
            }

            var categories = httpActionResponse.Response!;
            foreach (var category in categories!)
            {
                var found = producto!.ProductCategories!.FirstOrDefault(x => x.CategoryId == category.Id);
                if (found == null)
                {
                    nonSelectedCategories.Add(category);
                }
                else
                {
                    selectedCategories.Add(category);
                }
            }
            loading = false;
        }

        private async Task SaveChangesAsync()
        {
            var httpActionResponse = await Repository.PutAsync("/api/productos/full", productDTO);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                Snackbar.Add($"ERROR: {message}", Severity.Error);
                return;
            }

            Return();
        }

        private void Return()
        {
            productForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/productos");
        }

    }
}
