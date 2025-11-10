using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Code { get; set; } = string.Empty;

        // 🔗 Relaciones
        public Employee? EmployeeCreated { get; set; }
        public Employee? EmployeeUpdated { get; set; }

        public ICollection<SaleDetail>? SaleDetails { get; set; }
    }
}
