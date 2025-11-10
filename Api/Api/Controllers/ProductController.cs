using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Helpers;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Api.Helpers.ResponseHelper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {

        private int? UserId
        {
            get
            {
                // Intenta primero con el claim "sub"
                var claimValue = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                // Si no lo encuentra, prueba con NameIdentifier
                if (string.IsNullOrEmpty(claimValue))
                    claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(claimValue, out int id))
                    return id;

                return null;
            }
        }


        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Register(ProductRequestDto dto)
        {
            if (UserId == null)
                return Unauthorized(ErrorResponse<ProductResponseDto>("No se pudo identificar el usuario."));

            dto.CreatedBy = UserId.Value;

            var response = await productService.CrateProductAsync(dto);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }



        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Update(ProductRequestDto dto)
        {
            if (UserId == null)
                return Unauthorized(ErrorResponse<ProductResponseDto>("No se pudo identificar el usuario."));

            dto.UpdatedBy = UserId.Value;

            var response = await productService.UpdateProductAsync(dto);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Delete(int id)
        {
            var response = await productService.DeleteProductAsync(id);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductResponseDto>>>> GetList(
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var response = await productService.GetListProducAsync(search, page, pageSize);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>> GetById(int id)
        {
            var response = await productService.GetProductByIdAsync(id);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

    }
}
