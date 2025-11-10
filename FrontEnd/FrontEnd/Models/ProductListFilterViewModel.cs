using FrontEnd.Helper;
using FrontEnd.Models.Response;

namespace FrontEnd.Models
{
    public class ProductListFilterViewModel
    {
        public string? Search { get; set; }
        public PagedResult<ProductResponseDto>? Products { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
