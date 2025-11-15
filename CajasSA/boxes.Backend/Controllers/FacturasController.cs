using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : GenericController<Factura>
    {
        // falta implementar la unidad de trabajo para facturas 
        public FacturasController(IGenericUnitOfWork<Factura> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
