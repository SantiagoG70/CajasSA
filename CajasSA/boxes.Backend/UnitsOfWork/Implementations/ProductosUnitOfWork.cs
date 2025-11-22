using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Implementations
{
    public class ProductosUnitOfWork : GenericUnitOfWork<Producto>, IProductosUnitOfWork
    {
        private readonly IProductosRepository _productsRepository;

        public ProductosUnitOfWork(IGenericRepository<Producto> repository, IProductosRepository productsRepository) : base(repository)
        {
            _productsRepository = productsRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Producto>>> GetAsync(PaginationDTO pagination) => await _productsRepository.GetAsync(pagination);

        public async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _productsRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<Producto>> GetAsync(int id) => await _productsRepository.GetAsync(id);

        public override async Task<ActionResponse<Producto>> DeleteAsync(int id) => await _productsRepository.DeleteAsync(id);
        public async Task<ActionResponse<Producto>> AddFullAsync(ProductoDTO productDTO) => await _productsRepository.AddFullAsync(productDTO);

        public async Task<ActionResponse<Producto>> UpdateFullAsync(ProductoDTO productDTO) => await _productsRepository.UpdateFullAsync(productDTO);

        public async Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO) => await _productsRepository.AddImageAsync(imageDTO);

        public async Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO) => await _productsRepository.RemoveLastImageAsync(imageDTO);

    }
}
