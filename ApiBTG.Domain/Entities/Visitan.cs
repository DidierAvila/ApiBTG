using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    public class Visitan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdCliente { get; set; }
        
        [Required]
        public DateTime FechaVisita { get; set; }
        
        // Navigation properties
        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; } = null!;
        
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;
    }
} 