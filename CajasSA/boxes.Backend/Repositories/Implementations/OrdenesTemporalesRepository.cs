using boxes.Backend.Repositories.Interfaces;
using Boxes.Backend.Data;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace boxes.Backend.Repositories.Implementations
{
    public class OrdenesTemporalesRepository : GenericRepository<OrdenTemporal>, IOrdenesTemporalesRepository
    {
        private readonly DataContext _context;
        private readonly IUsuariosRepository _usersRepository;

        public OrdenesTemporalesRepository(DataContext context, IUsuariosRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<OrdenTemporalDTO>> AddFullAsync(string email, OrdenTemporalDTO temporalOrderDTO)
        {
            var product = await _context.Productos.FirstOrDefaultAsync(x => x.Id == temporalOrderDTO.ProductId);
            if (product == null)
            {
                return new ActionResponse<OrdenTemporalDTO>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            var user = await _usersRepository.GetUsuarioAsync(email);
            if (user == null)
            {
                return new ActionResponse<OrdenTemporalDTO>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            var temporalOrder = new OrdenTemporal
            {
                Product = product,
                Quantity = temporalOrderDTO.Quantity,
                Remarks = temporalOrderDTO.Remarks,
                User = user
            };

            try
            {
                _context.Add(temporalOrder);
                await _context.SaveChangesAsync();
                return new ActionResponse<OrdenTemporalDTO>
                {
                    WasSuccess = true,
                    Result = temporalOrderDTO
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<OrdenTemporalDTO>
                {
                    WasSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ActionResponse<IEnumerable<OrdenTemporal>>> GetAsync(string email)
        {
            var temporalOrders = await _context.OrdenesTemporales
                .Include(ts => ts.User!)
                .Include(ts => ts.Product!)
                .ThenInclude(p => p.ProductCategories!)
                .ThenInclude(pc => pc.Category)
                .Include(ts => ts.Product!)
                .ThenInclude(p => p.ProductImages)
                .Where(x => x.User!.Email == email)
                .ToListAsync();

            return new ActionResponse<IEnumerable<OrdenTemporal>>
            {
                WasSuccess = true,
                Result = temporalOrders
            };
        }

        public async Task<ActionResponse<int>> GetCountAsync(string email)
        {
            var count = await _context.OrdenesTemporales
                .Where(x => x.User!.Email == email)
                .SumAsync(x => x.Quantity);

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }

        public async Task<ActionResponse<OrdenTemporal>> PutFullAsync(OrdenTemporalDTO temporalOrderDTO)
        {
            var currentTemporalOrder = await _context.OrdenesTemporales.FirstOrDefaultAsync(x => x.Id == temporalOrderDTO.Id);
            if (currentTemporalOrder == null)
            {
                return new ActionResponse<OrdenTemporal>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            currentTemporalOrder!.Remarks = temporalOrderDTO.Remarks;
            currentTemporalOrder.Quantity = temporalOrderDTO.Quantity;

            _context.Update(currentTemporalOrder);
            await _context.SaveChangesAsync();
            return new ActionResponse<OrdenTemporal>
            {
                WasSuccess = true,
                Result = currentTemporalOrder
            };
        }

        public override async Task<ActionResponse<OrdenTemporal>> GetAsync(int id)
        {
            var temporalOrder = await _context.OrdenesTemporales
                .Include(ts => ts.User!)
                .Include(ts => ts.Product!)
                .ThenInclude(p => p.ProductCategories!)
                .ThenInclude(pc => pc.Category)
                .Include(ts => ts.Product!)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (temporalOrder == null)
            {
                return new ActionResponse<OrdenTemporal>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ActionResponse<OrdenTemporal>
            {
                WasSuccess = true,
                Result = temporalOrder
            };
        }


    }
}
