using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Domain.Dtos
{
    // DTO para obtener información de sucursal
    public class SucursalDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
    }

    // DTO para crear una nueva sucursal
    public class CreateSucursalDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(255, ErrorMessage = "La ciudad no puede exceder 255 caracteres")]
        public string Ciudad { get; set; } = string.Empty;
    }

    // DTO para actualizar una sucursal existente
    public class UpdateSucursalDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(255, ErrorMessage = "La ciudad no puede exceder 255 caracteres")]
        public string Ciudad { get; set; } = string.Empty;
    }
} 