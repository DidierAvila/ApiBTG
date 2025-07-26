using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    [Table(name: "Visita")]
    public class Visita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdCliente { get; set; }
        
        [Required]
        public DateTime FechaVisita { get; set; }
        
        [Required]
        [StringLength(50)]
        public string TipoAccion { get; set; } = string.Empty;
        
        // Navigation properties
        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; } = null!;
        
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;
    }
} 