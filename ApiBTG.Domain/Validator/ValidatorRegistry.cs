using ApiBTG.Domain.Entities;
using FluentValidation;

namespace ApiBTG.Domain.Validator
{
    /// <summary>
    /// Registro de validadores para todas las entidades del dominio
    /// </summary>
    public static class ValidatorRegistry
    {
        /// <summary>
        /// Obtiene el validador para la entidad Cliente
        /// </summary>
        public static IValidator<Cliente> ClienteValidator => new ClienteValidator();

        /// <summary>
        /// Obtiene el validador para la entidad Producto
        /// </summary>
        public static IValidator<Producto> ProductoValidator => new ProductoValidator();

        /// <summary>
        /// Obtiene el validador para la entidad Sucursal
        /// </summary>
        public static IValidator<Sucursal> SucursalValidator => new SucursalValidator();

        /// <summary>
        /// Obtiene el validador para la entidad Inscripcion
        /// </summary>
        public static IValidator<Inscripcion> InscripcionValidator => new InscripcionValidator();

        /// <summary>
        /// Obtiene el validador para la entidad Disponibilidad
        /// </summary>
        public static IValidator<Disponibilidad> DisponibilidadValidator => new DisponibilidadValidator();

        /// <summary>
        /// Obtiene el validador para la entidad Visitan
        /// </summary>
        public static IValidator<Visita> VisitanValidator => new VisitanValidator();

        /// <summary>
        /// Obtiene el validador para la entidad User
        /// </summary>
        public static IValidator<Usuario> UserValidator => new UserValidator();

        /// <summary>
        /// Obtiene el validador para la entidad Token
        /// </summary>
        public static IValidator<Token> TokenValidator => new TokenValidator();
    }
} 