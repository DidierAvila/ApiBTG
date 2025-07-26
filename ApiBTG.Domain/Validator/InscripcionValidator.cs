using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class InscripcionValidator : AbstractValidator<Inscripcion>
    {
        public InscripcionValidator()
        {
            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor a 0");

            RuleFor(x => x.IdDisponibilidad)
                .GreaterThan(0).WithMessage("El ID de la disponibilidad debe ser mayor a 0");

            // Validación personalizada para asegurar que el cliente y disponibilidad existan
            RuleFor(x => x.Cliente)
                .NotNull().WithMessage("El cliente es obligatorio")
                .When(x => x.Cliente != null);

            RuleFor(x => x.Disponibilidad)
                .NotNull().WithMessage("La disponibilidad es obligatoria")
                .When(x => x.Disponibilidad != null);
        }
    }
} 