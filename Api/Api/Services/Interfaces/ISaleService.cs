using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Helpers;
using static Api.Helpers.ResponseHelper;

namespace Api.Services.Interfaces
{
    public interface ISaleService
    {
        Task<ApiResponse<SaleResponseDto>> RegisterSaleAsync(SaleRequestDto dto);
    }
}
