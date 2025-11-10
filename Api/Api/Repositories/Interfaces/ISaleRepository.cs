using Api.Entities;
using Api.Helpers;
using static Api.Repositories.Implementations.SaleRepository;

namespace Api.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<int> RegisterSaleCartAsync(int employeeId, List<ProductSaleItem> products);
        Task<Sale?> GetSaleWithDetailsAsync(int id);
    }
}
