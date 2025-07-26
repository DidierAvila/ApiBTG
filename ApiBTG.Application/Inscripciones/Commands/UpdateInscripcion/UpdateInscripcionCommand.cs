using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.UpdateInscripcion
{
    public record UpdateInscripcionCommand : IRequest<InscripcionDto>
    {
        public int IdProducto { get; init; }
        public int IdCliente { get; init; }
        public int NewIdProducto { get; init; }
        public int NewIdCliente { get; init; }
    }
} 