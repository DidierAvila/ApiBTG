using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Visitas.Queries.GetVisitaById
{
    public record GetVisitaByIdQuery : IRequest<VisitaDto?>
    {
        public int IdSucursal { get; init; }
        public int IdCliente { get; init; }
    }
} 