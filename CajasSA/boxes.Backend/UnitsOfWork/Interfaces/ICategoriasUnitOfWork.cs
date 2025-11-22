using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Interfaces
{
    public interface ICategoriasUnitOfWork
    {

        Task<ActionResponse<IEnumerable<Categoria>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<Categoria>> GetComboAsync();

    }
}
