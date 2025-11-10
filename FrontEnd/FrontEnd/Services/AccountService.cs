using FrontEnd.Models;
using FrontEnd.Models.Response;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Services
{
    public class AccountService
    {
        private readonly ApiClientService _apiClient;

        public AccountService(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<ApiResponse<LoginResponseDto>?> LoginAsync(UserLoginViewModel model)
        {
            var response = await _apiClient.PostAsync<UserLoginViewModel, ApiResponse<LoginResponseDto>>("Auth/login", model);
            return response;
        }

        public async Task<ApiResponse<LoginResponseDto>?> RegisterAsync(RegisterViewModel model)
        {
            var response = await _apiClient.PostAsync<RegisterViewModel, ApiResponse<LoginResponseDto>>("Auth/register", model);
            return response;
        }
    }
}
