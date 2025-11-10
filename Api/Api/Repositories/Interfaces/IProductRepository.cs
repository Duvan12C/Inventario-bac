using Api.Entities;
using Api.Helpers;

namespace Api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProducAsync(Product product);
        Task<Product> UpdateProducAsync(Product product);
        Task<Product?> DeleteProductAsync(int id);
        Task<PagedResult<Product>> GetListProducAsync(string? search = null, int page = 1, int pageSize = 10);
        Task<Product?> GetProductByIdAsync(int id);
    }
}
