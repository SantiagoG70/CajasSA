using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : GenericController<Producto>
    {
        // Falta implementar la unidad de trabajo de productos
        public ProductosController(IGenericUnitOfWork<Producto> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
