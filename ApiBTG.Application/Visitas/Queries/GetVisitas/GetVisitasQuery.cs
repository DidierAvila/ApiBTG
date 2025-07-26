using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Visitas.Queries.GetVisitas
{
    public record GetVisitasQuery : IRequest<IEnumerable<VisitaDto>>
    {
    }
} 