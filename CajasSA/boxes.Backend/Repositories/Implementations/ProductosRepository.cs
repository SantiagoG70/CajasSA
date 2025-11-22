using boxes.Backend.Helpers;
using boxes.Backend.Repositories.Interfaces;
using Boxes.Backend.Data;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace boxes.Backend.Repositories.Implementations
{
    public class ProductosRepository: GenericRepository<Producto>, IProductosRepository
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public ProductosRepository(DataContext context, IFileStorage fileStorage) : base(context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public override async Task<ActionResponse<IEnumerable<Producto>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Productos
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategories)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(pagination.CategoryFilter))
            {
                queryable = queryable.Where(x => x.ProductCategories!
                    .Any(y => y.Category.Name.ToLower().Contains(pagination.CategoryFilter.ToLower())));
            }

            return new ActionResponse<IEnumerable<Producto>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Productos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(pagination.CategoryFilter))
            {
                queryable = queryable.Where(x => x.ProductCategories!
                    .Any(y => y.Category.Name.ToLower().Contains(pagination.CategoryFilter.ToLower())));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<Producto>> DeleteAsync(int id)
        {
            var product = await _context.Productos
                .Include(x => x.ProductCategories)
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = "Producto no encontrado"
                };
            }

            foreach (var productImage in product.ProductImages!)
            {
                await _fileStorage.RemoveFileAsync(productImage.Image, "productos");
            }

            try
            {
                _context.CategoriasProductos.RemoveRange(product.ProductCategories!);
                _context.ProductImages.RemoveRange(product.ProductImages!);
                _context.Productos.Remove(product);
                await _context.SaveChangesAsync();
                return new ActionResponse<Producto>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar el producto, porque tiene registros relacionados"
                };
            }
        }


        public override async Task<ActionResponse<Producto>> GetAsync(int id)
        {
            var product = await _context.Productos
                .Include(x => x.ProductImages)
                .Include(x => x.ProductCategories!)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            return new ActionResponse<Producto>
            {
                WasSuccess = true,
                Result = product
            };
        }

        public async Task<ActionResponse<Producto>> AddFullAsync(ProductoDTO productDTO)
        {
            try
            {
                var newProduct = new Producto
                {
                    Name = productDTO.Name,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    Quantity = productDTO.Quantity,
                    ProductCategories = new List<ProductCategory>(),
                    ProductImages = new List<ProductImage>()
                };

                foreach (var productImage in productDTO.ProductImages!)
                {
                    var photoProduct = Convert.FromBase64String(productImage);
                    newProduct.ProductImages.Add(new ProductImage { Image = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "Productos") });
                }

                foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                {
                    var category = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == productCategoryId);
                    if (category != null)
                    {
                        newProduct.ProductCategories.Add(new ProductCategory { Category = category });
                    }
                }


                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return new ActionResponse<Producto>
                {
                    WasSuccess = true,
                    Result = newProduct
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = "Ya existe un producto con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<Producto>> UpdateFullAsync(ProductoDTO productDTO)
        {
            try
            {
                var product = await _context.Productos
                    .Include(x => x.ProductCategories!)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == productDTO.Id);
                if (product == null)
                {
                    return new ActionResponse<Producto>
                    {
                        WasSuccess = false,
                        Message = "Producto no existe"
                    };
                }

                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.Quantity = productDTO.Quantity;

                _context.CategoriasProductos.RemoveRange(product.ProductCategories!);
                product.ProductCategories = new List<ProductCategory>();

                foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                {
                    var category = await _context.Categorias.FindAsync(productCategoryId);
                    if (category != null)
                    {
                        _context.CategoriasProductos.Add(new ProductCategory { CategoryId = category.Id, ProductoId = product.Id });
                    }
                }


                _context.Update(product);
                await _context.SaveChangesAsync();
                return new ActionResponse<Producto>
                {
                    WasSuccess = true,
                    Result = product
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = "Ya existe un producto con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Producto>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }
        public async Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO)
        {
            var product = await _context.Productos
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.ProductId);
            if (product == null)
            {
                return new ActionResponse<ImageDTO>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            for (int i = 0; i < imageDTO.Images.Count; i++)
            {
                if (!imageDTO.Images[i].StartsWith("https://"))
                {
                    var photoProduct = Convert.FromBase64String(imageDTO.Images[i]);
                    imageDTO.Images[i] = await _fileStorage.SaveFileAsync(photoProduct, ".jpg", "productos");
                    product.ProductImages!.Add(new ProductImage { Image = imageDTO.Images[i] });
                }
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            return new ActionResponse<ImageDTO>
            {
                WasSuccess = true,
                Result = imageDTO
            };
        }

        public async Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO)
        {
            var product = await _context.Productos
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.ProductId);
            if (product == null)
            {
                return new ActionResponse<ImageDTO>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            if (product.ProductImages is null || product.ProductImages.Count == 0)
            {
                return new ActionResponse<ImageDTO>
                {
                    WasSuccess = true,
                    Result = imageDTO
                };
            }

            var lastImage = product.ProductImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "productos");
            _context.ProductImages.Remove(lastImage);

            await _context.SaveChangesAsync();
            imageDTO.Images = product.ProductImages.Select(x => x.Image).ToList();
            return new ActionResponse<ImageDTO>
            {
                WasSuccess = true,
                Result = imageDTO
            };
        }

    }
}
