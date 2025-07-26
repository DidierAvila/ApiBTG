using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Commands.UpdateDisponibilidad
{
    public record UpdateDisponibilidadCommand : IRequest<DisponibilidadDto>
    {
        public int IdSucursal { get; init; }
        public int IdProducto { get; init; }
        public int NewIdSucursal { get; init; }
        public int NewIdProducto { get; init; }
        public decimal MontoMinimo { get; init; }
    }
} 