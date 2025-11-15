using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesFacturaController : GenericController<DetalleFactura>
    {
        //falta unidad de trabajo 
        public DetallesFacturaController(IGenericUnitOfWork<DetalleFactura> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
