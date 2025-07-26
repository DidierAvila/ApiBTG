using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Commands.CreateDisponibilidad
{
    public record CreateDisponibilidadCommand : IRequest<DisponibilidadDto>
    {
        public int IdSucursal { get; init; }
        public int IdProducto { get; init; }
        public decimal MontoMinimo { get; init; }
    }
} 