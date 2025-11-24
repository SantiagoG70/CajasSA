using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;
using System.Diagnostics.Metrics;

namespace boxes.Backend.UnitsOfWork.Implementations;

public class ProveedoresUnitOfWork : GenericUnitOfWork<Proveedor>, IProveedoresUnitOfWork
{
    private readonly IProveedoresRepository _proveedoresRepository;

    public ProveedoresUnitOfWork(IGenericRepository<Proveedor> repository, IProveedoresRepository proveedoresRepository) : base(repository)
    {
        _proveedoresRepository = proveedoresRepository;
    }

    public async Task<IEnumerable<Proveedor>> GetComboAsync() => await _proveedoresRepository.GetComboAsync();

    public override async Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync(PaginationDTO pagination) => await _proveedoresRepository.GetAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync() => await _proveedoresRepository.GetAsync();

    public override async Task<ActionResponse<Proveedor>> GetAsync(int id) => await _proveedoresRepository.GetAsync(id);
}