using ApiBTG.Application.Common.Interfaces;
using ApiBTG.Domain.Entities;
using ApiBTG.Domain.Enums;

namespace ApiBTG.Application.Notificaciones.Strategy
{
    public class SmsNotificationStrategy : INotificationStrategy
    {
        private readonly SmsNotificationService _smsService;
        public string Tipo => nameof(NotificacionPreferida.SMS);

        public SmsNotificationStrategy(SmsNotificationService smsService)
        {
            _smsService = smsService;
        }

        public async Task SendAsync(Usuario user, Disponibilidad disponibilidad, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(user.Telefono))
            {
                await _smsService.SendAsync(
                    user.Telefono,
                    "Inscripción exitosa",
                    $"Te has suscrito al fondo {disponibilidad.Producto!.Nombre} en la sucursal {disponibilidad.Sucursal!.Nombre}"
                );
            }
        }
    }

}
