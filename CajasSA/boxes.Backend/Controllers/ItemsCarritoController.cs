using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsCarritoController : GenericController<ItemCarrito>
    {
        // Falta implementar la unidad de trabajo de items carrito
        public ItemsCarritoController(IGenericUnitOfWork<ItemCarrito> unitOfWork) : base(unitOfWork)
        {
        }
    }
}