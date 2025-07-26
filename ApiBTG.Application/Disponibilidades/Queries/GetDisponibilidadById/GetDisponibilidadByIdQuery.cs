using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Queries.GetDisponibilidadById
{
    public record GetDisponibilidadByIdQuery : IRequest<DisponibilidadDto?>
    {
        public int IdSucursal { get; init; }
        public int IdProducto { get; init; }
    }
} 