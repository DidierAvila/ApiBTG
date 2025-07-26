using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.CreateInscripcion
{
    public record CreateInscripcionCommand : IRequest<InscripcionDto>
    {
        public int IdProducto { get; init; }
        public int IdCliente { get; init; }
    }
} 