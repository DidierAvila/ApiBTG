using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Domain.Dtos
{
    // DTO para obtener información de disponibilidad con datos relacionados
    public class DisponibilidadDto
    {
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        public decimal MontoMinimo { get; set; }
        public SucursalDto Sucursal { get; set; } = null!;
        public ProductoDto Producto { get; set; } = null!;
    }

    // DTO para obtener información básica de disponibilidad
    public class DisponibilidadSimpleDto
    {
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        public decimal MontoMinimo { get; set; }
    }

    // DTO para crear una nueva disponibilidad
    public class CreateDisponibilidadDto
    {
        [Required(ErrorMessage = "El ID de la sucursal es obligatorio")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El monto mínimo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto mínimo debe ser mayor a 0")]
        public decimal MontoMinimo { get; set; }
    }

    // DTO para actualizar una disponibilidad existente
    public class UpdateDisponibilidadDto
    {
        [Required(ErrorMessage = "El ID de la sucursal es obligatorio")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El monto mínimo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto mínimo debe ser mayor a 0")]
        public decimal MontoMinimo { get; set; }
    }
} 