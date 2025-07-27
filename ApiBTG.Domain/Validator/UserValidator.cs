using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class UserValidator : AbstractValidator<Usuario>
    {
        public UserValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(255).WithMessage("El nombre no puede exceder 255 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(255).WithMessage("El apellido no puede exceder 255 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El apellido solo puede contener letras y espacios");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .MaximumLength(100).WithMessage("El email no puede exceder 100 caracteres")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(x => x.Clave)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MaximumLength(20).WithMessage("La contraseña no puede exceder 20 caracteres")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$").WithMessage("La contraseña debe contener al menos una letra minúscula, una mayúscula y un número");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("El rol es obligatorio")
                .MaximumLength(255).WithMessage("El rol no puede exceder 255 caracteres")
                .Must(BeValidRole).WithMessage("El rol debe ser 'Admin', 'User' o 'Manager'");
        }

        private bool BeValidRole(string role)
        {
            var validRoles = new[] { "Admin", "User", "Manager" };
            return validRoles.Contains(role);
        }
    }
} 