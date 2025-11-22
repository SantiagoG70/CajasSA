using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Interfaces
{
    public interface IOrdenesTemporalesUnitOfWork
    {
        Task<ActionResponse<OrdenTemporalDTO>> AddFullAsync(string email, OrdenTemporalDTO temporalOrderDTO);

        Task<ActionResponse<IEnumerable<OrdenTemporal>>> GetAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);
        Task<ActionResponse<OrdenTemporal>> GetAsync(int id);

        Task<ActionResponse<OrdenTemporal>> PutFullAsync(OrdenTemporalDTO temporalOrderDTO);

        Task<ActionResponse<OrdenTemporal>> DeleteAsync(int id);
    }
}
