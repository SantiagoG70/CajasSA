using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : GenericController<Reportes>
    {
        //falta implementar la unidad de trabajo de reportes
        public ReportesController(IGenericUnitOfWork<Reportes> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
