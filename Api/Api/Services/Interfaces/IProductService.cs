using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Helpers;
using static Api.Helpers.ResponseHelper;

namespace Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponseDto>> CrateProductAsync(ProductRequestDto dto);
        Task<ApiResponse<ProductResponseDto>> UpdateProductAsync(ProductRequestDto dto);
        Task<ApiResponse<ProductResponseDto>> DeleteProductAsync(int id);
        Task<ApiResponse<PagedResult<ProductResponseDto>>> GetListProducAsync(string? search = null, int page = 1, int pageSize = 10);
        Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(int id);
    }
}
