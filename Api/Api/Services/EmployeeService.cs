using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Entities;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;
using static Api.Helpers.ResponseHelper;
namespace Api.Services
{
    public class EmployeeService(IEmployeeRepository repo, JwtService jwt, IMapper  mapper) : IEmployeeService
    {
        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto dto)
        {
            try
            {
                var user = await repo.GetByEmailAsync(dto.Email);
                if (user == null)
                {
                    return ErrorResponse<LoginResponseDto>("Usuario o contraseña incorrectos");
                }

                // Verificación de contraseña
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    return ErrorResponse<LoginResponseDto>("Usuario o contraseña incorrectos");
                }

                // Generar JWT
                var token = jwt.GenerateJwtToken(user);

                return SuccessResponse(new LoginResponseDto { Token = token, Nombre = user.Name }, "Login exitoso");

            }
            catch (Exception ex)
            {
                return ErrorResponse<LoginResponseDto>("Ocurrió un error al iniciar sesión. Intenta de nuevo más tarde.");

            }
        }


        public async Task<ApiResponse<LoginResponseDto>> RegisterAsync(RegisterRequestDto dto)
        {
            try
            {

                Employee employee = mapper.Map<Employee>(dto);
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
                var result = await repo.RegisterEmployeeAsync(employee);

                var token = jwt.GenerateJwtToken(result);

                return SuccessResponse(new LoginResponseDto { Token = token, Nombre = result.Name }, "Usuario registrado correctamente");

            }
            catch
            {
                return ErrorResponse<LoginResponseDto>("Ocurrió un error al registrar el usuario. Intenta de nuevo más tarde.");
            }
        }

    }
}
