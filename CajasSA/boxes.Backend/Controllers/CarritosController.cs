using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritosController : GenericController<Carrito>
    {
        //Falta unidad de trabajo
        public CarritosController(IGenericUnitOfWork<Carrito> unitOfWork) : base(unitOfWork)
        {
        }
    }
}