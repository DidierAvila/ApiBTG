using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Domain.Dtos
{
    // DTO para obtener información de visita con datos relacionados
    public class VisitaDto
    {
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaVisita { get; set; }
        public string TipoAccion { get; set; } = string.Empty;
        public SucursalDto Sucursal { get; set; } = null!;
        public ClienteDto Cliente { get; set; } = null!;
    }

    // DTO para obtener información básica de visita
    public class VisitanSimpleDto
    {
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaVisita { get; set; }
        public string TipoAccion { get; set; } = string.Empty;
    }

    // DTO para crear una nueva visita
    public class CreateVisitanDto
    {
        [Required(ErrorMessage = "El ID de la sucursal es obligatorio")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La fecha de visita es obligatoria")]
        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "El tipo de acción es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de acción no puede exceder 50 caracteres")]
        public string TipoAccion { get; set; } = string.Empty;
    }

    // DTO para actualizar una visita existente
    public class UpdateVisitanDto
    {
        [Required(ErrorMessage = "El ID es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de la sucursal es obligatorio")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La fecha de visita es obligatoria")]
        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "El tipo de acción es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de acción no puede exceder 50 caracteres")]
        public string TipoAccion { get; set; } = string.Empty;
    }
} 