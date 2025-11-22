using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Interfaces
{
    public interface IOrdenesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<Orden>> GetAsync(int id);

        Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrdenDTO orderDTO);

        Task<ActionResponse<Orden>> AddAsync(Orden order);

    }
}
