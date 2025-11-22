using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.Repositories.Interfaces
{
    public interface IProductosRepository
    {
        Task<ActionResponse<Producto>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Producto>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Producto>> DeleteAsync(int id);

        Task<ActionResponse<Producto>> AddFullAsync(ProductoDTO productDTO);

        Task<ActionResponse<Producto>> UpdateFullAsync(ProductoDTO productDTO);

        Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO);

        Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO);


    }
}
