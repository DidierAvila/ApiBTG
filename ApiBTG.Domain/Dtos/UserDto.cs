using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Domain.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(255, ErrorMessage = "El apellido no puede exceder 255 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es requerido")]
        [StringLength(255, ErrorMessage = "El rol no puede exceder 255 caracteres")]
        public string Role { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "La preferencia de notificación no puede exceder 10 caracteres")]
        public string NotificacionPreferida { get; set; } = "Email";

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string? Telefono { get; set; }

        // Propiedad calculada para el nombre completo
        public string FullName => $"{FirstName} {LastName}".Trim();
    }

    public class CreateUserDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(255, ErrorMessage = "El apellido no puede exceder 255 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 20 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es requerido")]
        [StringLength(255, ErrorMessage = "El rol no puede exceder 255 caracteres")]
        public string Role { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "La preferencia de notificación no puede exceder 10 caracteres")]
        public string NotificacionPreferida { get; set; } = "Email";

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string? Telefono { get; set; }
    }

    public class UpdateUserDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder 255 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(255, ErrorMessage = "El apellido no puede exceder 255 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 20 caracteres")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [StringLength(255, ErrorMessage = "El rol no puede exceder 255 caracteres")]
        public string Role { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "La preferencia de notificación no puede exceder 10 caracteres")]
        public string NotificacionPreferida { get; set; } = "Email";

        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string? Telefono { get; set; }
    }
} 