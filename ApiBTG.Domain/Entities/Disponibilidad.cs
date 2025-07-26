using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    [Table(name: "Disponibilidad")]
    public class Disponibilidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoMinimo { get; set; }
        
        // Navigation properties
        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; } = null!;
        
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; } = null!;
    }
} 