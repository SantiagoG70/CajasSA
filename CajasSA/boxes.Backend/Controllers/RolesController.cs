using boxes.Backend.Controllers;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Boxes.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : GenericController<Rol>
{
    public RolesController(IGenericUnitOfWork<Rol> unit) : base(unit)
    {
        int culo = 0;   
    }
}