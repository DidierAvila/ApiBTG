using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.CreateInscripcion
{
    public class CreateInscripcionCommandHandler : IRequestHandler<CreateInscripcionCommand, InscripcionSimpleDto>
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IDisponibilidadRepository _disponibilidadRepository;
        private readonly IVisitaRepository _visitaRepository;

        public CreateInscripcionCommandHandler(
            IInscripcionRepository inscripcionRepository,
            IClienteRepository clienteRepository,
            IDisponibilidadRepository disponibilidadRepository,
            IVisitaRepository visitaRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
            _disponibilidadRepository = disponibilidadRepository;
            _visitaRepository = visitaRepository;

        }

        public async Task<InscripcionSimpleDto> Handle(CreateInscripcionCommand request, CancellationToken cancellationToken)
        {
            // Verificar que el cliente existe
            var cliente = await _clienteRepository.GetByID(request.IdCliente, cancellationToken);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente con ID {request.IdCliente} no encontrado");

            // Verificar que la disponibilidad existe
            var disponibilidad = await _disponibilidadRepository.GetByID(request.IdDisponibilidad, cancellationToken);
            if (disponibilidad == null)
                throw new KeyNotFoundException($"Disponibilidad con ID {request.IdDisponibilidad} no encontrada");

            // Verificar que no existe ya una inscripción para ese cliente y esa disponibilidad
            var exists = await _inscripcionRepository.ExistsInscripcionAsync(request.IdCliente, request.IdDisponibilidad, cancellationToken);
            if (exists)
                throw new InvalidOperationException($"Ya existe una inscripción para el cliente {request.IdCliente} en la disponibilidad {request.IdDisponibilidad}");

            // Verificar que el cliente cumple con el monto mínimo
            if (cliente.Monto < disponibilidad.MontoMinimo)
                throw new InvalidOperationException($"El cliente {cliente.Nombre} no tiene saldo disponible para vincularse al fondo {disponibilidad.Producto.Nombre} en la sucursal {disponibilidad.Sucursal.Nombre}");

            var inscripcion = new Inscripcion
            {
                IdCliente = request.IdCliente,
                IdDisponibilidad = request.IdDisponibilidad
            };

            var createdInscripcion = await _inscripcionRepository.Create(inscripcion, cancellationToken);

            // Registrar la inscripción en el historial de transacciones
            var visita = new Visita
            {
                IdSucursal = disponibilidad.IdSucursal,
                IdCliente = request.IdCliente,
                FechaVisita = DateTime.UtcNow,
                TipoAccion = "Apertura",
            };
            await _visitaRepository.Create(visita, cancellationToken);

            // Actualizar el monto del cliente
            cliente.Monto -= disponibilidad.MontoMinimo;
            await _clienteRepository.Update(cliente, cancellationToken);

            return new InscripcionSimpleDto
            {
                Id = createdInscripcion.Id,
                IdCliente = createdInscripcion.IdCliente,
                IdDisponibilidad = createdInscripcion.IdDisponibilidad, 
            };
        }
    }
} 