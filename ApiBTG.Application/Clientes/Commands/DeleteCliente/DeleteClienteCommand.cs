using MediatR;

namespace ApiBTG.Application.Clientes.Commands.DeleteCliente
{
    public record DeleteClienteCommand : IRequest<bool>
    {
        public int Id { get; init; }
    }
} 