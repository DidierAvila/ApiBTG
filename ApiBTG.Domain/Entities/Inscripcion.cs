using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBTG.Domain.Entities
{
    [Table(name: "Inscripcion")]
    public class Inscripcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdDisponibilidad { get; set; }
        
        // Navigation properties
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;
        
        [ForeignKey("IdDisponibilidad")]
        public virtual Disponibilidad Disponibilidad { get; set; } = null!;
    }
} 