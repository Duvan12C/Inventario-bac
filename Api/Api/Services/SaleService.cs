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
                // Ejecuta el SP y obtiene el ID de la venta creada
                var idSale = await repo.RegisterSaleCartAsync(dto.EmployeeId, dto.Products);

                if (idSale == 0)
                {
                    return ErrorResponse<SaleResponseDto>("No se pudo registrar la venta");
                }

                // Trae la venta y detalle desde la BD
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