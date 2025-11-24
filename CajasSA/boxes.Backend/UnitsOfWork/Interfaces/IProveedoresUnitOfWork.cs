using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Interfaces;

public interface IProveedoresUnitOfWork
{
    Task<IEnumerable<Proveedor>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Proveedor>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync();
}