using MediatR;

namespace ApiBTG.Application.Visitas.Commands.DeleteVisita
{
    public record DeleteVisitaCommand : IRequest<bool>
    {
        public int IdSucursal { get; init; }
        public int IdCliente { get; init; }
    }
} 