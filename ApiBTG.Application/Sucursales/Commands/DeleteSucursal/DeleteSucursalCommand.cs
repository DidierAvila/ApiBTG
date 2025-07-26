using MediatR;

namespace ApiBTG.Application.Sucursales.Commands.DeleteSucursal
{
    public record DeleteSucursalCommand : IRequest<bool>
    {
        public int Id { get; init; }
    }
} 