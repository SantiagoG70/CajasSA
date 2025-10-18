using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Implementations;

public class ProveedoresUnitOfWork : GenericUnitOfWork<Proveedor>, IProveedoresUnitOfWork
{
    private readonly IProveedoresRepository _proveedoresRepository;

    public ProveedoresUnitOfWork(IGenericRepository<Proveedor> repository, IProveedoresRepository proveedoresRepository) : base(repository)
    {
        _proveedoresRepository = proveedoresRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync() => await _proveedoresRepository.GetAsync();

    public override async Task<ActionResponse<Proveedor>> GetAsync(int id) => await _proveedoresRepository.GetAsync(id);
}