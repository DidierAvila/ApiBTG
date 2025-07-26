using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Apellidos { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Ciudad { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        
        // Navigation properties para las relaciones many-to-many
        public virtual ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public virtual ICollection<Visitan> Visitas { get; set; } = new List<Visitan>();
    }
} 