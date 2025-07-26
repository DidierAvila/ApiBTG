using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class InscripcionValidator : AbstractValidator<Inscripcion>
    {
        public InscripcionValidator()
        {
            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a 0");

            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor a 0");

            // Validación personalizada para asegurar que el producto y cliente existan
            RuleFor(x => x.Producto)
                .NotNull().WithMessage("El producto es obligatorio")
                .When(x => x.Producto != null);

            RuleFor(x => x.Cliente)
                .NotNull().WithMessage("El cliente es obligatorio")
                .When(x => x.Cliente != null);
        }
    }
} 