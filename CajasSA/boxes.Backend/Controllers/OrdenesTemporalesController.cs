using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace boxes.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesTemporalesController : GenericController<OrdenTemporal>
    {
        private readonly IOrdenesTemporalesUnitOfWork _ordenesTemporalesUnitOfWork;

        public OrdenesTemporalesController(IGenericUnitOfWork<OrdenTemporal> unitOfWork, IOrdenesTemporalesUnitOfWork ordenesTemporalesUnitOfWork) : base(unitOfWork)
        {
            _ordenesTemporalesUnitOfWork = ordenesTemporalesUnitOfWork;
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _ordenesTemporalesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("my")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _ordenesTemporalesUnitOfWork.GetAsync(User.Identity!.Name!);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountAsync()
        {
            var action = await _ordenesTemporalesUnitOfWork.GetCountAsync(User.Identity!.Name!);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostAsync(OrdenTemporalDTO temporalOrderDTO)
        {
            var action = await _ordenesTemporalesUnitOfWork.AddFullAsync(User.Identity!.Name!, temporalOrderDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutFullAsync(OrdenTemporalDTO temporalOrderDTO)
        {
            var action = await _ordenesTemporalesUnitOfWork.PutFullAsync(temporalOrderDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }
    }
}