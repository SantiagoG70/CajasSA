using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.Repositories.Interfaces
{
    public interface IOrdenesTemporalesRepository
    {
        Task<ActionResponse<OrdenTemporalDTO>> AddFullAsync(string email, OrdenTemporalDTO dto);

        Task<ActionResponse<IEnumerable<OrdenTemporal>>> GetAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);
        Task<ActionResponse<OrdenTemporal>> GetAsync(int id);

        Task<ActionResponse<OrdenTemporal>> PutFullAsync(OrdenTemporalDTO temporalOrderDTO);


    }
}
