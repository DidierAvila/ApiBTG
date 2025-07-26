using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Visitas.Commands.UpdateVisita
{
    public class UpdateVisitaCommandHandler : IRequestHandler<UpdateVisitaCommand, VisitaDto>
    {
        private readonly IVisitaRepository _visitaRepository;

        public UpdateVisitaCommandHandler(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        public async Task<VisitaDto> Handle(UpdateVisitaCommand request, CancellationToken cancellationToken)
        {
            var visita = await _visitaRepository.GetVisitaByIdsAsync(request.IdSucursal, request.IdCliente, cancellationToken);

            if (visita == null)
            {
                throw new KeyNotFoundException($"Visita con Sucursal ID {request.IdSucursal} y Cliente ID {request.IdCliente} no encontrada");
            }

            visita.FechaVisita = request.FechaVisita;
            visita.TipoAccion = request.TipoAccion;

            await _visitaRepository.Update(visita, cancellationToken);

            return new VisitaDto
            {
                Id = visita.Id,
                IdSucursal = visita.IdSucursal,
                IdCliente = visita.IdCliente,
                FechaVisita = visita.FechaVisita,
                TipoAccion = visita.TipoAccion,
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