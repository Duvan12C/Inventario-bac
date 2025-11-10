using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Entities;
using Api.Helpers;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;
using static Api.Helpers.ResponseHelper;

namespace Api.Services
{
    public class ProductService(IProductRepository repo, IMapper mapper) : IProductService
    {
        public async Task<ApiResponse<ProductResponseDto>> CrateProductAsync(ProductRequestDto dto)
        {
            try
            {
                if (dto == null)
                    return ErrorResponse<ProductResponseDto>("La información está nula");

                Product product = mapper.Map<Product>(dto);
                
                var result = await repo.CreateProducAsync(product);

                ProductResponseDto productDto = mapper.Map<ProductResponseDto>(result);

                return SuccessResponse(productDto, "Producto creado correctamente");
            }
            catch (Exception ex)
            {
                return ErrorResponse<ProductResponseDto>(
                    "Ocurrió un error al crear el producto. Intenta de nuevo más tarde."
                );
            }
        }


        public async Task<ApiResponse<ProductResponseDto>> UpdateProductAsync(ProductRequestDto dto)
        {
            try
            {
                if (dto == null)
                    return ErrorResponse<ProductResponseDto>("La información está nula");

                Product product = mapper.Map<Product>(dto);
                product.UpdatedAt = DateTime.UtcNow;

                var result = await repo.UpdateProducAsync(product);

                if (result == null)
                    return ErrorResponse<ProductResponseDto>("No se encontró el producto para actualizar");

                ProductResponseDto productDto = mapper.Map<ProductResponseDto>(result);

                return SuccessResponse(productDto, "Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                // 🔍 Podés loguear el error si querés:
                // logger.LogError(ex, "Error actualizando producto");

                return ErrorResponse<ProductResponseDto>(
                    "Ocurrió un error al actualizar el producto. Intenta de nuevo más tarde."
                );
            }
        }



        public async Task<ApiResponse<ProductResponseDto>> DeleteProductAsync(int id)
        {
            try
            {
                var result = await repo.DeleteProductAsync(id);

                ProductResponseDto productDto = mapper.Map<ProductResponseDto>(result);

                return new ApiResponse<ProductResponseDto>
                {
                    Success = true,
                    Message = "Producto eliminado correctamente",
                    Data = productDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>
                {
                    Success = false,
                    Message = "Ocurrió un error al eliminar el producto. Intenta de nuevo más tarde.",
                    Data = null
                };
            }
        }


        public async Task<ApiResponse<PagedResult<ProductResponseDto>>> GetListProducAsync(string? search = null, int page = 1, int pageSize = 10)
        {
            try
            {
                var result = await repo.GetListProducAsync(search, page, pageSize);

                List<ProductResponseDto> productDtos = mapper.Map<List<ProductResponseDto>>(result.Items);

                var pagedResponse = new PagedResult<ProductResponseDto>
                {
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    TotalCount = result.TotalCount,
                    TotalPages = result.TotalPages,
                    Items = productDtos
                };

                return new ApiResponse<PagedResult<ProductResponseDto>>
                {
                    Success = true,
                    Message = "Lista de productos obtenida correctamente",
                    Data = pagedResponse
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedResult<ProductResponseDto>>
                {
                    Success = false,
                    Message = $"Error al obtener la lista de productos: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var result = await repo.GetProductByIdAsync(id);

                ProductResponseDto productDto = mapper.Map<ProductResponseDto>(result);

                return new ApiResponse<ProductResponseDto>
                {
                    Success = true,
                    Message = "Producto obetenido correctamente",
                    Data = productDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>
                {
                    Success = false,
                    Message = "Ocurrió un error al obtener el producto. Intenta de nuevo más tarde.",
                    Data = null
                };
            }
        }

    }
}
