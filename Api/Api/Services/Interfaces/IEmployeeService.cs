using Api.Dtos.Request;
using Api.Dtos.Response;
using static Api.Helpers.ResponseHelper;

namespace Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto dto);
        Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterRequestDto dto);
    }
}
