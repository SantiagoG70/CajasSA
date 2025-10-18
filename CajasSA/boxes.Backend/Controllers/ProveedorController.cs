using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : GenericController<Proveedor>
{
    private readonly IProveedoresUnitOfWork _proveedoresUnitOfWork;

    public ProveedoresController(IGenericUnitOfWork<Proveedor> unit, IProveedoresUnitOfWork proveedoresUnitOfWork) : base(unit)
    {
        _proveedoresUnitOfWork = proveedoresUnitOfWork;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _proveedoresUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _proveedoresUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }
}