using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Domain.Dtos
{
    // DTO para obtener información de producto con datos relacionados
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string TipoProducto { get; set; } = string.Empty;
    }

    // DTO para obtener información básica de producto
    public class ProductoSimpleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string TipoProducto { get; set; } = string.Empty;
    }

    // DTO para crear un nuevo producto
    public class CreateProductoDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de producto es obligatorio")]
        [StringLength(255, ErrorMessage = "El tipo de producto no puede exceder 255 caracteres")]
        public string TipoProducto { get; set; } = string.Empty;
    }

    // DTO para actualizar un producto existente
    public class UpdateProductoDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de producto es obligatorio")]
        [StringLength(255, ErrorMessage = "El tipo de producto no puede exceder 255 caracteres")]
        public string TipoProducto { get; set; } = string.Empty;
    }
} 