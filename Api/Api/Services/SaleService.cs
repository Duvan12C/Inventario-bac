using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Helpers;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;
using static Api.Helpers.ResponseHelper;

namespace Api.Services
{
    public class SaleService(ISaleRepository repo, IMapper mapper) : ISaleService
    {

        public async Task<ApiResponse<SaleResponseDto>> RegisterSaleAsync(SaleRequestDto dto)
        {
            try
            {
                var idSale = await repo.RegisterSaleCartAsync(dto.EmployeeId, dto.Products);

                if (idSale == 0)
                {
                    return ErrorResponse<SaleResponseDto>("No se pudo registrar la venta");
                }

                var sale = await repo.GetSaleWithDetailsAsync(idSale);
                SaleResponseDto saleDto = mapper.Map<SaleResponseDto>(sale);

                return SuccessResponse(saleDto, "Venta registrada correctamente");

            }
            catch (Exception ex)
            {
                return new ApiResponse<SaleResponseDto>
                {
                    Success = false,
                    Message = $"Error al registrar la venta: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}