using MediatR;
using ApiBTG.Domain.Dtos;

namespace ApiBTG.Application.Clientes.Queries.GetClientesConInscripcionesEnSucursalesVisitadas
{
    public record GetClientesConInscripcionesEnSucursalesVisitadasQuery : IRequest<IEnumerable<ClienteInscripcionDto>>
    {
        // Opcional: filtrar por cliente específico
        public int? ClienteId { get; init; }
        
        // Opcional: filtrar por sucursal específica
        public int? SucursalId { get; init; }
    }
} 