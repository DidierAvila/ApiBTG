using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Sucursales.Queries.GetSucursalById
{
    public record GetSucursalByIdQuery : IRequest<SucursalDto?>
    {
        public int Id { get; init; }
    }
} 