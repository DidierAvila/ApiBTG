using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Clientes.Queries.GetClienteById
{
    public record GetClienteByIdQuery : IRequest<ClienteDto?>
    {
        public int Id { get; init; }
    }
} 