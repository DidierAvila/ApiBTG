using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Domain.Dtos
{
    // DTO para obtener información de inscripción con datos relacionados
    public class InscripcionDto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdCliente { get; set; }
        public ClienteDto Cliente { get; set; } = null!;
        public ProductoDto Producto { get; set; } = null!;
    }

    // DTO para obtener información básica de inscripción
    public class InscripcionSimpleDto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdCliente { get; set; }
    }

    // DTO para crear una nueva inscripción
    public class CreateInscripcionDto
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int IdCliente { get; set; }
    }

    // DTO para actualizar una inscripción existente
    public class UpdateInscripcionDto
    {
        [Required(ErrorMessage = "El ID es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int IdCliente { get; set; }
    }
} 