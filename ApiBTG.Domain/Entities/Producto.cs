using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string TipoProducto { get; set; } = string.Empty;
        
        // Navigation properties para las relaciones many-to-many
        public virtual ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public virtual ICollection<Disponibilidad> Disponibilidades { get; set; } = new List<Disponibilidad>();
    }
} 