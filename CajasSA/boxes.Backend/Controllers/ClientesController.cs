using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : GenericController<Cliente>
    {
        //falta unidad de trabajo 
        public ClientesController(IGenericUnitOfWork<Cliente> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
