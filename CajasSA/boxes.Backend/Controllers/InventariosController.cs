using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventariosController : GenericController<Inventario>
    {
        // Falta implementar la unidad de trabajo de inventarios
        public InventariosController(IGenericUnitOfWork<Inventario> unitOfWork) : base(unitOfWork)
        {
        }
    }
}