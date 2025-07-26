using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Sucursales.Commands.CreateSucursal
{
    public record CreateSucursalCommand : IRequest<SucursalDto>
    {
        public string Nombre { get; init; } = string.Empty;
        public string Ciudad { get; init; } = string.Empty;
    }
} 