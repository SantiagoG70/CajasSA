using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProveedorController : GenericController<Proveedor>
{
    public ProveedorController(IGenericUnitOfWork<Proveedor> unitOfWork) : base(unitOfWork)
    {

    }
}