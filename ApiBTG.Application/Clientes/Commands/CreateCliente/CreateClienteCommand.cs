using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Clientes.Commands.CreateCliente
{
    public record CreateClienteCommand : IRequest<ClienteDto>
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public int? UsuarioId { get; set; }
    }
} 