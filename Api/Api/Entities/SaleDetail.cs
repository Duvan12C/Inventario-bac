using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{

    [Table("SaleDetail")]
    public class SaleDetail
    {
        [Key]
        public int IdSaleDetail { get; set; }
        public int IdSale { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        //Relaciones
        public Sale? Sale { get; set; }
        public Product? Product { get; set; }
    }
}
