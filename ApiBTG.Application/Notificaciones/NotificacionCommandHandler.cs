using ApiBTG.Application.Common.Interfaces;
using ApiBTG.Application.Users;
using ApiBTG.Domain.Enums;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Notificaciones
{
    public class NotificacionCommandHandler : IRequestHandler<NotificacionCommand, string>
    {
        private readonly EmailNotificationService _emailNotificationService;
        private readonly SmsNotificationService _smsNotificationService;
        private readonly IUserService _userService;
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public NotificacionCommandHandler(
            EmailNotificationService emailNotificationService,
            SmsNotificationService smsNotificationService,
            IUserService userService,
            IDisponibilidadRepository disponibilidadRepository)
        {
            _emailNotificationService = emailNotificationService;
            _smsNotificationService = smsNotificationService;
            _userService = userService;
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<string> Handle(NotificacionCommand request, CancellationToken cancellationToken)
        {
            // Obtener usuario relacionado a través de la relación Cliente-Usuario
            string result = "Notificación enviada correctamente";
            var user = await _userService.Get(request.UsuarioId, cancellationToken);
            if (user != null)
            {
                // Consultar la disponibilidad para obtener el nombre del producto y sucursal
                var disponibilidad = await _disponibilidadRepository.GetByID(request.DisponibilidadId, cancellationToken);
                if (disponibilidad == null)
                {
                    return "Disponibilidad no encontrada";
                }
                disponibilidad = await _disponibilidadRepository.GetDisponibilidadByIdsAsync(disponibilidad.IdProducto, disponibilidad.IdSucursal, cancellationToken);
                
                // Temporalmente usar solo email hasta resolver el problema con Twilio
                if (user.NotificacionPreferida == NotificacionPreferida.Email.ToString() && !string.IsNullOrEmpty(user.Email))
                {
                    await _emailNotificationService.SendAsync(user.Email, "Inscripción exitosa", $"Te has suscrito al fondo {disponibilidad!.Producto!.Nombre} en la sucursal {disponibilidad!.Sucursal!.Nombre}");
                }
                else if (user.NotificacionPreferida == NotificacionPreferida.SMS.ToString() && !string.IsNullOrEmpty(user.Telefono))
                {
                    // Enviar SMS solo si la preferencia es SMS y el teléfono está disponible
                    await _smsNotificationService.SendAsync(user.Telefono, "Inscripción exitosa", $"Te has suscrito al fondo {disponibilidad!.Producto!.Nombre} en la sucursal {disponibilidad!.Sucursal!.Nombre}");
                }
            }
            return result;
        }
    }
}
