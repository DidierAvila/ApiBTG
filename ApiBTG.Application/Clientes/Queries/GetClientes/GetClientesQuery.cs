using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Clientes.Queries.GetClientes
{
    public record GetClientesQuery : IRequest<IEnumerable<ClienteDto>>
    {
    }
} 