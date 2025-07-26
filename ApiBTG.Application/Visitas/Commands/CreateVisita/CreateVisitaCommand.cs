using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Visitas.Commands.CreateVisita
{
    public record CreateVisitaCommand : IRequest<VisitaDto>
    {
        public int IdSucursal { get; init; }
        public int IdCliente { get; init; }
        public DateTime FechaVisita { get; init; }
        public required string TipoAccion { get; init; }
    }
} 