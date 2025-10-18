using boxes.Backend.Repositories.Interfaces;
using Boxes.Backend.Data;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace boxes.Backend.Repositories.Implementations;

public class ProveedoresRepository : GenericRepository<Proveedor>, IProveedoresRepository
{
    private readonly DataContext _context;

    public ProveedoresRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<Proveedor>>> GetAsync()
    {
        var Proveedores = await _context.Proveedores
            .Include(c => c.Name)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Proveedor>>
        {
            WasSuccess = true,
            Result = Proveedores
        };
    }

    public override async Task<ActionResponse<Proveedor>> GetAsync(int id)
    {
        var proveedor = await _context.Proveedores
             .Include(c => c.Phone!)
             .FirstOrDefaultAsync(c => c.Id == id);

        if (proveedor == null)
        {
            return new ActionResponse<Proveedor>
            {
                WasSuccess = false,
                Message = "País no existe"
            };
        }

        return new ActionResponse<Proveedor>
        {
            WasSuccess = true,
            Result = proveedor
        };
    }
}