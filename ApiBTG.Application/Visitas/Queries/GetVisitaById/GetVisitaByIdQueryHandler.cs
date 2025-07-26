using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Visitas.Queries.GetVisitaById
{
    public class GetVisitaByIdQueryHandler : IRequestHandler<GetVisitaByIdQuery, VisitaDto?>
    {
        private readonly IVisitaRepository _visitaRepository;

        public GetVisitaByIdQueryHandler(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        public async Task<VisitaDto?> Handle(GetVisitaByIdQuery request, CancellationToken cancellationToken)
        {
            var visita = await _visitaRepository.GetVisitaByIdsAsync(request.IdSucursal, request.IdCliente, cancellationToken);

            if (visita == null)
                return null;

            return new VisitaDto
            {
                Id = visita.Id,
                IdSucursal = visita.IdSucursal,
                IdCliente = visita.IdCliente,
                FechaVisita = visita.FechaVisita,
                Sucursal = new SucursalDto
                {
                    Id = visita.Sucursal.Id,
                    Nombre = visita.Sucursal.Nombre,
                    Ciudad = visita.Sucursal.Ciudad
                },
                Cliente = new ClienteDto
                {
                    Id = visita.Cliente.Id,
                    Nombre = visita.Cliente.Nombre,
                    Apellidos = visita.Cliente.Apellidos,
                    Ciudad = visita.Cliente.Ciudad,
                    Monto = visita.Cliente.Monto
                }
            };
        }
    }
} 