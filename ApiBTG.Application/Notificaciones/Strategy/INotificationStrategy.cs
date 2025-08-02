using ApiBTG.Domain.Entities;

namespace ApiBTG.Application.Notificaciones.Strategy
{
    public interface INotificationStrategy
    {
        string Tipo { get; } // Ej: "Email", "SMS", "Push"
        Task SendAsync(Usuario user, Disponibilidad disponibilidad, CancellationToken cancellationToken);
    }
}
