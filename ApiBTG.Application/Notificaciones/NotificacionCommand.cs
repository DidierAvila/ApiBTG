using MediatR;

namespace ApiBTG.Application.Notificaciones
{
    public class NotificacionCommand : IRequest<string>
    {
        public required int UsuarioId { get; set; }
        public int DisponibilidadId { get; set; }
    }
}
