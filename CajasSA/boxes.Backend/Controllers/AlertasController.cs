using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : GenericController<Alerta>
    {
        //falta unidad de trabajo
        public AlertasController(IGenericUnitOfWork<Alerta> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
