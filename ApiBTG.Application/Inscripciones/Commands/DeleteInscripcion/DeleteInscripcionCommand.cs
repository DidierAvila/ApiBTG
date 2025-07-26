using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.DeleteInscripcion
{
    public record DeleteInscripcionCommand : IRequest<bool>
    {
        public int IdProducto { get; init; }
        public int IdCliente { get; init; }
    }
} 