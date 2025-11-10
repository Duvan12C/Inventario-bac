using Api.Helpers;

namespace Api.Dtos.Request
{
    public class SaleRequestDto
    {
        public int EmployeeId { get; set; }
        public List<ProductSaleItem> Products { get; set; } = new();
    }
}
