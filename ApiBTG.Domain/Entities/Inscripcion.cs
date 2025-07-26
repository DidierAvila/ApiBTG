using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    public class Inscripcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdCliente { get; set; }
        
        // Navigation properties
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; } = null!;
        
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;
    }
} 