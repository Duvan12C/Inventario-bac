using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
