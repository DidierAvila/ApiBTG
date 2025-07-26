using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Clientes.Commands.UpdateCliente
{
    public record UpdateClienteCommand : IRequest<ClienteDto>
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public int? UsuarioId { get; set; }
    }
} 