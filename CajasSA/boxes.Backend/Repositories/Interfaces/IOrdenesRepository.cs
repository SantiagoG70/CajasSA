using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.Repositories.Interfaces
{
    public interface IOrdenesRepository
    {
        Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<Orden>> GetAsync(int id);

        Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrdenDTO orderDTO);

    }
}
