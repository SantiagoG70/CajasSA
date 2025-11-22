using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Boxes.Shared.Enums;
using Boxes.Shared.Responses;

namespace boxes.Backend.Helpers
{
    public class OrdenesHelper: IOrdenesHelper
    {
        private readonly IUsuariosUnitOfWork _usersUnitOfWork;
        private readonly IOrdenesTemporalesUnitOfWork _temporalOrdersUnitOfWork;
        private readonly IProductosUnitOfWork _productsUnitOfWork;
        private readonly IOrdenesUnitOfWork _ordersUnitOfWork;

        public OrdenesHelper(IUsuariosUnitOfWork usersUnitOfWork, IOrdenesTemporalesUnitOfWork temporalOrdersUnitOfWork, IProductosUnitOfWork productsUnitOfWork, IOrdenesUnitOfWork ordersUnitOfWork)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _temporalOrdersUnitOfWork = temporalOrdersUnitOfWork;
            _productsUnitOfWork = productsUnitOfWork;
            _ordersUnitOfWork = ordersUnitOfWork;
        }

        public async Task<ActionResponse<bool>> ProcessOrderAsync(string email, string remarks)
        {
            var user = await _usersUnitOfWork.GetUsuarioAsync(email);
            if (user == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido"
                };
            }

            var actionTemporalOrders = await _temporalOrdersUnitOfWork.GetAsync(email);
            if (!actionTemporalOrders.WasSuccess)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "No hay detalle en la orden"
                };
            }

            var temporalOrders = actionTemporalOrders.Result as List<OrdenTemporal>;
            var response = await CheckInventoryAsync(temporalOrders!);
            if (!response.WasSuccess)
            {
                return response;
            }

            var order = new Orden
            {
                Date = DateTime.UtcNow,
                User = user,
                Remarks = remarks,
                OrderDetails = new List<DetalleOrden>(),
                OrderStatus = OrdenStatus.New
            };

            foreach (var temporalOrder in temporalOrders!)
            {
                order.OrderDetails.Add(new DetalleOrden
                {
                    Product = temporalOrder.Product,
                    Quantity = temporalOrder.Quantity,
                    Remarks = temporalOrder.Remarks,
                });

                var actionProduct = await _productsUnitOfWork.GetAsync(temporalOrder.Product!.Id);
                if (actionProduct.WasSuccess)
                {
                    var product = actionProduct.Result;
                    if (product != null)
                    {
                        product.Quantity -= (int)temporalOrder.Quantity;
                        await _productsUnitOfWork.UpdateAsync(product);
                    }
                }

                await _temporalOrdersUnitOfWork.DeleteAsync(temporalOrder.Id);
            }

            await _ordersUnitOfWork.AddAsync(order);
            return response;
        }

        private async Task<ActionResponse<bool>> CheckInventoryAsync(List<OrdenTemporal> temporalOrders)
        {
            var response = new ActionResponse<bool>() { WasSuccess = true };
            foreach (var temporalOrder in temporalOrders)
            {
                var actionProduct = await _productsUnitOfWork.GetAsync(temporalOrder.Product!.Id);
                if (!actionProduct.WasSuccess)
                {
                    response.WasSuccess = false;
                    response.Message = $"El producto {temporalOrder.Product!.Id}, ya no está disponible";
                    return response;
                }

                var product = actionProduct.Result;
                if (product == null)
                {
                    response.WasSuccess = false;
                    response.Message = $"El producto {temporalOrder.Product!.Id}, ya no está disponible";
                    return response;
                }

                if (product.Quantity < temporalOrder.Quantity)
                {
                    response.WasSuccess = false;
                    response.Message = $"Lo sentimos no tenemos existencias suficientes del producto {temporalOrder.Product!.Name}, para tomar su pedido. Por favor disminuir la cantidad o sustituirlo por otro.";
                    return response;
                }
            }
            return response;
        }

    }
}
