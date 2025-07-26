using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    [Table(name: "Sucursal")]
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Ciudad { get; set; } = string.Empty;
        
        // Navigation properties para las relaciones many-to-many
        public virtual ICollection<Disponibilidad> Disponibilidades { get; set; } = new List<Disponibilidad>();
        public virtual ICollection<Visita> Visitas { get; set; } = new List<Visita>();
    }
} 