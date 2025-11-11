using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductCreateEditViewModel
    {
        public int? IdProduct { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [Display(Name = "Código del producto")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre del producto")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "{0} debe ser mayor que cero.")]
        [Display(Name = "Precio")]
        public decimal? Price { get; set; } 

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        [Display(Name = "Cantidad")]
        public int? Quantity { get; set; }

    }
}
