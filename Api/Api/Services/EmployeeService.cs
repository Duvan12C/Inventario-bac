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
                    return new ApiResponse<LoginResponseDto>
                    {
                        Success = false,
                        Message = "Usuario o contraseña incorrectos",
                        Data = null
                    };
                }

                // Verificación de contraseña
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    return new ApiResponse<LoginResponseDto>
                    {
                        Success = false,
                        Message = "Usuario o contraseña incorrectos",
                        Data = null
                    };
                }

                // Generar JWT
                var token = jwt.GenerateJwtToken(user);

                return new ApiResponse<LoginResponseDto>
                {
                    Success = true,
                    Message = "Login exitoso",
                    Data = new LoginResponseDto { Token = token, Nombre = user.Name }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Ocurrió un error al iniciar sesión. Intenta de nuevo más tarde.",
                    Data = null
                };
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

                return new ApiResponse<LoginResponseDto>
                {
                    Success = true,
                    Message = "Usuario registrado correctamente",
                    Data = new LoginResponseDto
                    {
                        Token = token,
                        Nombre = result.Name,
                    }
                };
            }
            catch
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Ocurrió un error al registrar el usuario. Intenta de nuevo más tarde.",
                    Data = null
                };
            }
        }

    }
}
