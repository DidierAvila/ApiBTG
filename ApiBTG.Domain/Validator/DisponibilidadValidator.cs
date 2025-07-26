using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class DisponibilidadValidator : AbstractValidator<Disponibilidad>
    {
        public DisponibilidadValidator()
        {
            RuleFor(x => x.IdSucursal)
                .GreaterThan(0).WithMessage("El ID de la sucursal debe ser mayor a 0");

            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a 0");

            RuleFor(x => x.MontoMinimo)
                .GreaterThan(0).WithMessage("El monto mínimo debe ser mayor a 0")
                .LessThanOrEqualTo(999999999.99m).WithMessage("El monto mínimo no puede exceder 999,999,999.99");

            // Validación personalizada para asegurar que la sucursal y producto existan
            RuleFor(x => x.Sucursal)
                .NotNull().WithMessage("La sucursal es obligatoria")
                .When(x => x.Sucursal != null);

            RuleFor(x => x.Producto)
                .NotNull().WithMessage("El producto es obligatorio")
                .When(x => x.Producto != null);
        }
    }
} 