using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Queries.GetDisponibilidades
{
    public record GetDisponibilidadesQuery : IRequest<IEnumerable<DisponibilidadDto>>
    {
    }
} 