using ApiBTG.Domain.Dtos;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El ID debe ser mayor que 0");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("El nombre es requerido")
                .MaximumLength(255)
                .WithMessage("El nombre no puede exceder 255 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("El apellido es requerido")
                .MaximumLength(255)
                .WithMessage("El apellido no puede exceder 255 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El email es requerido")
                .EmailAddress()
                .WithMessage("El formato del email no es válido")
                .MaximumLength(100)
                .WithMessage("El email no puede exceder 100 caracteres");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("El rol es requerido")
                .MaximumLength(255)
                .WithMessage("El rol no puede exceder 255 caracteres");

            RuleFor(x => x.NotificacionPreferida)
                .MaximumLength(10)
                .WithMessage("La preferencia de notificación no puede exceder 10 caracteres")
                .Must(x => string.IsNullOrEmpty(x) || x == "Email" || x == "SMS")
                .WithMessage("La preferencia de notificación debe ser 'Email' o 'SMS'");

            RuleFor(x => x.Telefono)
                .MaximumLength(20)
                .WithMessage("El teléfono no puede exceder 20 caracteres");
        }
    }

    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("El nombre es requerido")
                .MaximumLength(255)
                .WithMessage("El nombre no puede exceder 255 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("El apellido es requerido")
                .MaximumLength(255)
                .WithMessage("El apellido no puede exceder 255 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El email es requerido")
                .EmailAddress()
                .WithMessage("El formato del email no es válido")
                .MaximumLength(100)
                .WithMessage("El email no puede exceder 100 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("La contraseña es requerida")
                .MinimumLength(6)
                .WithMessage("La contraseña debe tener al menos 6 caracteres")
                .MaximumLength(20)
                .WithMessage("La contraseña no puede exceder 20 caracteres");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("El rol es requerido")
                .MaximumLength(255)
                .WithMessage("El rol no puede exceder 255 caracteres");

            RuleFor(x => x.NotificacionPreferida)
                .MaximumLength(10)
                .WithMessage("La preferencia de notificación no puede exceder 10 caracteres")
                .Must(x => string.IsNullOrEmpty(x) || x == "Email" || x == "SMS")
                .WithMessage("La preferencia de notificación debe ser 'Email' o 'SMS'");

            RuleFor(x => x.Telefono)
                .MaximumLength(20)
                .WithMessage("El teléfono no puede exceder 20 caracteres");
        }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("El nombre es requerido")
                .MaximumLength(255)
                .WithMessage("El nombre no puede exceder 255 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("El apellido es requerido")
                .MaximumLength(255)
                .WithMessage("El apellido no puede exceder 255 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El email es requerido")
                .EmailAddress()
                .WithMessage("El formato del email no es válido")
                .MaximumLength(100)
                .WithMessage("El email no puede exceder 100 caracteres");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("La contraseña debe tener al menos 6 caracteres")
                .MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("La contraseña no puede exceder 20 caracteres");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("El rol es requerido")
                .MaximumLength(255)
                .WithMessage("El rol no puede exceder 255 caracteres");

            RuleFor(x => x.NotificacionPreferida)
                .MaximumLength(10)
                .WithMessage("La preferencia de notificación no puede exceder 10 caracteres")
                .Must(x => string.IsNullOrEmpty(x) || x == "Email" || x == "SMS")
                .WithMessage("La preferencia de notificación debe ser 'Email' o 'SMS'");

            RuleFor(x => x.Telefono)
                .MaximumLength(20)
                .WithMessage("El teléfono no puede exceder 20 caracteres");
        }
    }
} 