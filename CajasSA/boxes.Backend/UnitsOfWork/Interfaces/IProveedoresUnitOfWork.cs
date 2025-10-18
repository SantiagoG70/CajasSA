using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Interfaces;

public interface IProveedoresUnitOfWork
{
    Task<ActionResponse<Proveedor>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync();
}