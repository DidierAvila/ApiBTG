using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Sucursales.Queries.GetSucursales
{
    public record GetSucursalesQuery : IRequest<IEnumerable<SucursalDto>>
    {
    }
} 