using boxes.Backend.UnitsOfWork.Implementations;
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
    public class ProductosController : GenericController<Producto>
    {
        private readonly IProductosUnitOfWork _unitOfWork;

        public ProductosController(IGenericUnitOfWork<Producto> unitOfWork, IProductosUnitOfWork productosUnitOfWork) : base(unitOfWork)
        {
            _unitOfWork = productosUnitOfWork;
        }

        [HttpGet("paginated")]
        [AllowAnonymous]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _unitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _unitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _unitOfWork.GetAsync(id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(ProductoDTO productDTO)
        {
            var action = await _unitOfWork.AddFullAsync(productDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpPost("addImages")]
        public async Task<IActionResult> PostAddImagesAsync(ImageDTO imageDTO)
        {
            var action = await _unitOfWork.AddImageAsync(imageDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPost("removeLastImage")]
        public async Task<IActionResult> PostRemoveLastImageAsync(ImageDTO imageDTO)
        {
            var action = await _unitOfWork.RemoveLastImageAsync(imageDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutFullAsync(ProductoDTO productDTO)
        {
            var action = await _unitOfWork.UpdateFullAsync(productDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _unitOfWork.DeleteAsync(id);
            if (!action.WasSuccess)
            {
                if (action.Message!.Contains("registros relacionados"))
                {
                    return BadRequest(action.Message);
                }
                return NotFound();
            }
            return NoContent();
        }
    }
}