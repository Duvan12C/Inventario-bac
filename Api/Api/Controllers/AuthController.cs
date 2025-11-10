using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Api.Helpers.ResponseHelper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Register(RegisterRequestDto dto)
        {
            var response = await employeeService.RegisterAsync(dto);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(LoginRequestDto dto)
        {
            var response = await employeeService.LoginAsync(dto);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}

