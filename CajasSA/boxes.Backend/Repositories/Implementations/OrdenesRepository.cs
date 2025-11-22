using boxes.Backend.Helpers;
using boxes.Backend.Repositories.Interfaces;
using Boxes.Backend.Data;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Enums;
using Boxes.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace boxes.Backend.Repositories.Implementations
{
    public class OrdenesRepository : GenericRepository<Orden>, IOrdenesRepository
    {
        private readonly DataContext _context;
        private readonly IUsuariosRepository _usersRepository;

        public OrdenesRepository(DataContext context, IUsuariosRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination)
        {
            var user = await _usersRepository.GetUsuarioAsync(email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Orden>>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido",
                };
            }

            var queryable = _context.Ordenes
                .Include(s => s.User!)
                .Include(s => s.OrderDetails!)
                .ThenInclude(sd => sd.Product)
                .AsQueryable();

            var isAdmin = await _usersRepository.IsUsuarioInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == email);
            }

            return new ActionResponse<IEnumerable<Orden>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderByDescending(x => x.Date)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination)
        {
            var user = await _usersRepository.GetUsuarioAsync(email);
            if (user == null)
            {
                return new ActionResponse<int>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido",
                };
            }

            var queryable = _context.Ordenes.AsQueryable();

            var isAdmin = await _usersRepository.IsUsuarioInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == email);
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)totalPages
            };
        }

        public override async Task<ActionResponse<Orden>> GetAsync(int id)
        {
            var order = await _context.Ordenes
                .Include(s => s.User!)
                .Include(s => s.OrderDetails!)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (order == null)
            {
                return new ActionResponse<Orden>
                {
                    WasSuccess = false,
                    Message = "Pedido no existe"
                };
            }

            return new ActionResponse<Orden>
            {
                WasSuccess = true,
                Result = order
            };
        }

        public async Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrdenDTO orderDTO)
        {
            var user = await _usersRepository.GetUsuarioAsync(email);
            if (user == null)
            {
                return new ActionResponse<Orden>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            var isAdmin = await _usersRepository.IsUsuarioInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin && orderDTO.OrdenStatus != OrdenStatus.Cancelled)
            {
                return new ActionResponse<Orden>
                {
                    WasSuccess = false,
                    Message = "Solo permitido para administradores."
                };
            }

            var order = await _context.Ordenes
                .Include(s => s.OrderDetails)
                .FirstOrDefaultAsync(s => s.Id == orderDTO.Id);
            if (order == null)
            {
                return new ActionResponse<Orden>
                {
                    WasSuccess = false,
                    Message = "Pedido no existe"
                };
            }

            if (orderDTO.OrdenStatus == OrdenStatus.Cancelled)
            {
                await ReturnStockAsync(order);
            }

            order.OrderStatus = orderDTO.OrdenStatus;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return new ActionResponse<Orden>
            {
                WasSuccess = true,
                Result = order
            };
        }

        private async Task ReturnStockAsync(Orden order)
        {
            foreach (var orderDetail in order.OrderDetails!)
            {
                var product = await _context.Productos.FirstOrDefaultAsync(p => p.Id == orderDetail.ProductId);
                if (product != null)
                {
                    product.Quantity += (int)orderDetail.Quantity;
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
