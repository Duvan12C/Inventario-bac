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


    }
}
