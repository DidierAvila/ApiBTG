using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class VisitanValidator : AbstractValidator<Visitan>
    {
        public VisitanValidator()
        {
            RuleFor(x => x.IdSucursal)
                .GreaterThan(0).WithMessage("El ID de la sucursal debe ser mayor a 0");

            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor a 0");

            RuleFor(x => x.FechaVisita)
                .NotEmpty().WithMessage("La fecha de visita es obligatoria")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de visita no puede ser futura");

            // Validación personalizada para asegurar que la sucursal y cliente existan
            RuleFor(x => x.Sucursal)
                .NotNull().WithMessage("La sucursal es obligatoria")
                .When(x => x.Sucursal != null);

            RuleFor(x => x.Cliente)
                .NotNull().WithMessage("El cliente es obligatorio")
                .When(x => x.Cliente != null);
        }
    }
} 