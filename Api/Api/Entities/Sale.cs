using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    [Table("Sale")]
    public class Sale
    {
        [Key]
        public int IdSale { get; set; }
        public DateTime DateSale { get; set; } = DateTime.Now;
        public int IdSeller { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 🔗 Relaciones
        public Employee? Seller { get; set; }
        public ICollection<SaleDetail>? SaleDetails { get; set; }
    }
}
