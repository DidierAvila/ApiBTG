using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(255).WithMessage("El nombre no puede exceder 255 caracteres")
                .Matches(@"^[a-zA-Z0-9_\-\.]+$").WithMessage("El nombre solo puede contener letras, números, guiones, puntos y guiones bajos");

            RuleFor(x => x.TipoProducto)
                .NotEmpty().WithMessage("El tipo de producto es obligatorio")
                .MaximumLength(255).WithMessage("El tipo de producto no puede exceder 255 caracteres")
                .Must(BeValidTipoProducto).WithMessage("El tipo de producto debe ser 'FPV' o 'FIC'");
        }

        private bool BeValidTipoProducto(string tipoProducto)
        {
            return tipoProducto == "FPV" || tipoProducto == "FIC";
        }
    }
} 