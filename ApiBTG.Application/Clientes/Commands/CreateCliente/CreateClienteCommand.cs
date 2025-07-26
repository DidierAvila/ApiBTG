using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Clientes.Commands.CreateCliente
{
    public record CreateClienteCommand : IRequest<ClienteDto>
    {
        public string Nombre { get; init; } = string.Empty;
        public string Apellidos { get; init; } = string.Empty;
        public string Ciudad { get; init; } = string.Empty;
        public decimal Monto { get; init; }
    }
} 