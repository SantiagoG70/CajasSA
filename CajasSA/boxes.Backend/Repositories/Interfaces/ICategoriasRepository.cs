using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.Repositories.Interfaces
{
    public interface ICategoriasRepository
    {
        Task<ActionResponse<IEnumerable<Categoria>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Categoria>> GetComboAsync();
    }
}
