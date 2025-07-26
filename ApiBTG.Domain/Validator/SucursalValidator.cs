using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class SucursalValidator : AbstractValidator<Sucursal>
    {
        public SucursalValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(255).WithMessage("El nombre no puede exceder 255 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s\-\.]+$").WithMessage("El nombre solo puede contener letras, números, espacios, guiones y puntos");

            RuleFor(x => x.Ciudad)
                .NotEmpty().WithMessage("La ciudad es obligatoria")
                .MaximumLength(255).WithMessage("La ciudad no puede exceder 255 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("La ciudad solo puede contener letras y espacios");
        }
    }
} 