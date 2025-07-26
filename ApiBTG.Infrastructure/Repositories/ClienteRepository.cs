using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using ApiBTG.Domain.Dtos;

namespace ApiBTG.Infrastructure.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetClientesWithInscripcionesAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(c => c.Inscripciones)
                .ThenInclude(i => i.Disponibilidad)
                .ThenInclude(d => d.Producto)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Cliente>> GetClientesWithVisitasAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(c => c.Visitas)
                .ThenInclude(v => v.Sucursal)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ClienteInscripcionDto>> GetClientesConInscripcionesEnSucursalesVisitadasAsync(
            int? clienteId = null, 
            int? sucursalId = null, 
            CancellationToken cancellationToken = default)
        {
            var query = _context.Clientes
                .Include(c => c.Inscripciones)
                    .ThenInclude(i => i.Disponibilidad)
                        .ThenInclude(d => d.Sucursal)
                .Include(c => c.Inscripciones)
                    .ThenInclude(i => i.Disponibilidad)
                        .ThenInclude(d => d.Producto)
                .Include(c => c.Visitas)
                .AsQueryable();

            // Aplicar filtros opcionales
            if (clienteId.HasValue)
                query = query.Where(c => c.Id == clienteId.Value);

            if (sucursalId.HasValue)
                query = query.Where(c => c.Inscripciones.Any(i => i.Disponibilidad.IdSucursal == sucursalId.Value));

            var clientes = await query.ToListAsync(cancellationToken);

            var resultado = new List<ClienteInscripcionDto>();

            foreach (var cliente in clientes)
            {
                // Obtener sucursales visitadas por este cliente
                var sucursalesVisitadas = cliente.Visitas
                    .Select(v => v.IdSucursal)
                    .Distinct()
                    .ToHashSet();

                // Filtrar inscripciones que estén en sucursales visitadas
                var inscripcionesValidas = cliente.Inscripciones
                    .Where(i => sucursalesVisitadas.Contains(i.Disponibilidad.IdSucursal));

                foreach (var inscripcion in inscripcionesValidas)
                {
                    // Obtener la última visita a esta sucursal
                    var ultimaVisita = cliente.Visitas
                        .Where(v => v.IdSucursal == inscripcion.Disponibilidad.IdSucursal)
                        .OrderByDescending(v => v.FechaVisita)
                        .FirstOrDefault();

                    resultado.Add(new ClienteInscripcionDto
                    {
                        ClienteId = cliente.Id,
                        NombreCliente = cliente.Nombre,
                        ApellidosCliente = cliente.Apellidos,
                        CiudadCliente = cliente.Ciudad,
                        SucursalId = inscripcion.Disponibilidad.IdSucursal,
                        NombreSucursal = inscripcion.Disponibilidad.Sucursal?.Nombre ?? "N/A",
                        CiudadSucursal = inscripcion.Disponibilidad.Sucursal?.Ciudad ?? "N/A",
                        ProductoId = inscripcion.Disponibilidad.IdProducto,
                        NombreProducto = inscripcion.Disponibilidad.Producto?.Nombre ?? "N/A",
                        TipoProducto = inscripcion.Disponibilidad.Producto?.TipoProducto ?? "N/A",
                        MontoMinimo = inscripcion.Disponibilidad.MontoMinimo,
                        FechaInscripcion = DateTime.UtcNow, // Nota: necesitarías agregar este campo a la entidad Inscripcion
                        UltimaVisita = ultimaVisita?.FechaVisita ?? DateTime.MinValue
                    });
                }
            }

            return resultado.OrderBy(r => r.NombreCliente).ThenBy(r => r.NombreSucursal);
        }
    }
}
