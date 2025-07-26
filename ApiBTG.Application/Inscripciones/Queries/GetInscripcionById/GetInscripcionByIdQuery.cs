using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Queries.GetInscripcionById
{
    public record GetInscripcionByIdQuery : IRequest<InscripcionDto?>
    {
        public int IdProducto { get; init; }
        public int IdCliente { get; init; }
    }
} 