using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Api.Helpers.ResponseHelper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController(ISaleService saleService) : ControllerBase
    {
        private int? UserId
        {
            get
            {
                var claimValue = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(claimValue))
                    claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(claimValue, out int id))
                    return id;

                return null;
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<SaleResponseDto>>> RegisterSale([FromBody] SaleRequestDto request)
        {
            if (request == null || request.Products == null || request.Products.Count == 0)
                return BadRequest(ErrorResponse<SaleResponseDto>("La venta no contiene productos."));

            if (UserId == null)
                return Unauthorized(ErrorResponse<ProductResponseDto>("No se pudo identificar el usuario."));

            var response = await saleService.RegisterSaleAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
