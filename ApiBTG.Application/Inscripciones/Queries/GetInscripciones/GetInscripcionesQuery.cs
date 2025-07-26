using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Queries.GetInscripciones
{
    public record GetInscripcionesQuery : IRequest<IEnumerable<InscripcionDto>>
    {
    }
} 