using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Visitas.Commands.UpdateVisita
{
    public record UpdateVisitaCommand : IRequest<VisitaDto>
    {
        public int IdSucursal { get; init; }
        public int IdCliente { get; init; }
        public int NewIdSucursal { get; init; }
        public int NewIdCliente { get; init; }
        public DateTime FechaVisita { get; init; }
        public string TipoAccion { get; init; }
    }
} 