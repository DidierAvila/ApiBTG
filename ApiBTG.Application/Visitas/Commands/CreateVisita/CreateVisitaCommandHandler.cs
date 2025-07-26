using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Visitas.Commands.CreateVisita
{
    public class CreateVisitaCommandHandler : IRequestHandler<CreateVisitaCommand, VisitaDto>
    {
        private readonly IVisitaRepository _visitaRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IClienteRepository _clienteRepository;

        public CreateVisitaCommandHandler(
            IVisitaRepository visitaRepository,
            ISucursalRepository sucursalRepository,
            IClienteRepository clienteRepository)
        {
            _visitaRepository = visitaRepository;
            _sucursalRepository = sucursalRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<VisitaDto> Handle(CreateVisitaCommand request, CancellationToken cancellationToken)
        {
            // Verificar que la sucursal existe
            var sucursal = await _sucursalRepository.GetByID(request.IdSucursal, cancellationToken);
            if (sucursal == null)
            {
                throw new KeyNotFoundException($"Sucursal con ID {request.IdSucursal} no encontrada");
            }

            // Verificar que el cliente existe
            var cliente = await _clienteRepository.GetByID(request.IdCliente, cancellationToken);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {request.IdCliente} no encontrado");
            }

            // Verificar que no existe ya una visita
            var exists = await _visitaRepository.ExistsVisitaAsync(request.IdSucursal, request.IdCliente, cancellationToken);
            if (exists)
            {
                throw new InvalidOperationException($"Ya existe una visita para la sucursal {request.IdSucursal} y el cliente {request.IdCliente}");
            }

            var visita = new Visita
            {
                IdSucursal = request.IdSucursal,
                IdCliente = request.IdCliente,
                FechaVisita = request.FechaVisita,
                TipoAccion = request.TipoAccion
            };

            var createdVisita = await _visitaRepository.Create(visita, cancellationToken);

            return new VisitaDto
            {
                Id = createdVisita.Id,
                IdSucursal = createdVisita.IdSucursal,
                IdCliente = createdVisita.IdCliente,
                FechaVisita = createdVisita.FechaVisita,
                TipoAccion = createdVisita.TipoAccion,
                Sucursal = new SucursalDto
                {
                    Id = sucursal.Id,
                    Nombre = sucursal.Nombre,
                    Ciudad = sucursal.Ciudad
                },
                Cliente = new ClienteDto
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellidos = cliente.Apellidos,
                    Ciudad = cliente.Ciudad,
                    Monto = cliente.Monto
                }
            };
        }
    }
} 