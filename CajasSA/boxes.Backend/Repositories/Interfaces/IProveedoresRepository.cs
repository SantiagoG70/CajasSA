using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.Repositories.Interfaces;

public interface IProveedoresRepository
{
    Task<ActionResponse<Proveedor>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync();
}