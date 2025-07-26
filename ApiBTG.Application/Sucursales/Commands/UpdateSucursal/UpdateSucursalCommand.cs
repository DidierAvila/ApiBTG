using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Sucursales.Commands.UpdateSucursal
{
    public record UpdateSucursalCommand : IRequest<SucursalDto>
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Ciudad { get; init; } = string.Empty;
    }
} 