using ApiBTG.Application.Common.Interfaces;
using ApiBTG.Domain.Entities;
using ApiBTG.Domain.Enums;

namespace ApiBTG.Application.Notificaciones.Strategy
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        public string Tipo => nameof(NotificacionPreferida.Email);
        private readonly EmailNotificationService _emailService;

        public EmailNotificationStrategy(EmailNotificationService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendAsync(Usuario user, Disponibilidad disponibilidad, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                await _emailService.SendAsync(
                    user.Email,
                    "Inscripción exitosa",
                    $"Te has suscrito al fondo {disponibilidad.Producto!.Nombre} en la sucursal {disponibilidad.Sucursal!.Nombre}"
                );
            }
        }
    }

}
