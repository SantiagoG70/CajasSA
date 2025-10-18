using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.Repositories.Interfaces;

public interface IProveedoresRepository
{
    Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Proveedor>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync();
}