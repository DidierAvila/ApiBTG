using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Visitas.Queries.GetVisitas
{
    public class GetVisitasQueryHandler : IRequestHandler<GetVisitasQuery, IEnumerable<VisitaDto>>
    {
        private readonly IVisitaRepository _visitaRepository;

        public GetVisitasQueryHandler(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        public async Task<IEnumerable<VisitaDto>> Handle(GetVisitasQuery request, CancellationToken cancellationToken)
        {
            var visitas = await _visitaRepository.GetVisitasWithDetailsAsync(cancellationToken);

            return visitas.Select(v => new VisitaDto
            {
                Id = v.Id,
                IdSucursal = v.IdSucursal,
                IdCliente = v.IdCliente,
                FechaVisita = v.FechaVisita,
                TipoAccion = v.TipoAccion,
                Sucursal = new SucursalDto
                {
                    Id = v.Sucursal.Id,
                    Nombre = v.Sucursal.Nombre,
                    Ciudad = v.Sucursal.Ciudad
                },
                Cliente = new ClienteDto
                {
                    Id = v.Cliente.Id,
                    Nombre = v.Cliente.Nombre,
                    Apellidos = v.Cliente.Apellidos,
                    Ciudad = v.Cliente.Ciudad,
                    Monto = v.Cliente.Monto
                }
            });
        }
    }
} 