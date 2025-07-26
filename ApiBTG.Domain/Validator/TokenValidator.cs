using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class TokenValidator : AbstractValidator<Token>
    {
        public TokenValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El ID del usuario debe ser mayor a 0");

            RuleFor(x => x.TokenValue)
                .NotEmpty().WithMessage("El token es obligatorio")
                .When(x => x.TokenValue != null);

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("La fecha de creación es obligatoria")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de creación no puede ser futura");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("La fecha de vencimiento es obligatoria")
                .GreaterThan(DateTime.Now).WithMessage("La fecha de vencimiento debe ser futura");

            RuleFor(x => x.ExpirationDate)
                .GreaterThan(x => x.CreatedDate).WithMessage("La fecha de vencimiento debe ser posterior a la fecha de creación");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("El estado es obligatorio");
        }
    }
} 