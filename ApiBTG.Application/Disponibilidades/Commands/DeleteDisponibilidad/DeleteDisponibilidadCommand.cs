using MediatR;

namespace ApiBTG.Application.Disponibilidades.Commands.DeleteDisponibilidad
{
    public record DeleteDisponibilidadCommand : IRequest<bool>
    {
        public int IdSucursal { get; init; }
        public int IdProducto { get; init; }
    }
} 