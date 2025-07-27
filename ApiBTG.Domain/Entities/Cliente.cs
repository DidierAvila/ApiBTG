using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    [Table(name: "Cliente")]
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
        
        // Relación con Usuario
        public int? UsuarioId { get; set; }
        
        // Navigation properties para las relaciones many-to-many
        public virtual ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public virtual ICollection<Visita> Visitas { get; set; } = new List<Visita>();
        
        // Navigation property para la relación con User
        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }
    }
} 