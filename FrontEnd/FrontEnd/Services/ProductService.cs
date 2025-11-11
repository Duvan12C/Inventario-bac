using FrontEnd.Models;
using FrontEnd.Models.Response;
using FrontEnd.Helper;

namespace FrontEnd.Services
{
    public class ProductService
    {

        private readonly ApiClientService _apiClient;

        public ProductService(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }


        public async Task<ApiResponse<PagedResult<ProductResponseDto>>?> ProductListAsync(string? search, int page, int pageSize)
        {
            var query = new Dictionary<string, string>
                {
                    { "search", search ?? "" },
                    { "page", page.ToString() },
                    { "pageSize", pageSize.ToString() }
                };

            return await _apiClient.GetListAsync<ApiResponse<PagedResult<ProductResponseDto>>>("Product/list", query, true);
        }

        public async Task<ApiResponse<ProductResponseDto>?> CreateAsync(ProductCreateEditViewModel model)
        {
            var response = await _apiClient.PostAsync<ProductCreateEditViewModel, ApiResponse<ProductResponseDto>>("Product/create", model, true);
            return response;
        }

        public async Task<ApiResponse<ProductResponseDto>?> UpdateAsync(ProductCreateEditViewModel model)
        {
            var response = await _apiClient.PutAsync<ProductCreateEditViewModel, ApiResponse<ProductResponseDto>>("Product/update", model, true);
            return response;
        }


        public async Task<ApiResponse<ProductResponseDto>?> GetByIdAsync(int id)
        {
            string endpoint = $"Product/{id}";
            return await _apiClient.GetAsync<ApiResponse<ProductResponseDto>>(endpoint, useToken: true);
        }


    }
}
