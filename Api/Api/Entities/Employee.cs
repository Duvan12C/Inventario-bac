using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? IdRol { get; set; }


        public ICollection<Product>? ProductsCreated { get; set; }
        public ICollection<Product>? ProductsUpdated { get; set; }
        public ICollection<Sale>? Sales { get; set; }


        [ForeignKey("IdRol")]
        public Rol? Role { get; set; }
    }
}
