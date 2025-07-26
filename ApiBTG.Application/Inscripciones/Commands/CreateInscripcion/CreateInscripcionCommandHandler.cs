using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.CreateInscripcion
{
    public class CreateInscripcionCommandHandler : IRequestHandler<CreateInscripcionCommand, InscripcionDto>
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public CreateInscripcionCommandHandler(
            IInscripcionRepository inscripcionRepository,
            IClienteRepository clienteRepository,
            IDisponibilidadRepository disponibilidadRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<InscripcionDto> Handle(CreateInscripcionCommand request, CancellationToken cancellationToken)
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

            // Actualizar el monto del cliente
            cliente.Monto -= disponibilidad.MontoMinimo;
            await _clienteRepository.Update(cliente, cancellationToken);

            return new InscripcionDto
            {
                Id = createdInscripcion.Id,
                IdCliente = createdInscripcion.IdCliente,
                IdDisponibilidad = createdInscripcion.IdDisponibilidad,
                Cliente = new ClienteDto
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellidos = cliente.Apellidos,
                    Ciudad = cliente.Ciudad,
                    Monto = cliente.Monto
                },
                Disponibilidad = new DisponibilidadDto
                {
                    Id = disponibilidad.Id,
                    IdSucursal = disponibilidad.IdSucursal,
                    IdProducto = disponibilidad.IdProducto,
                    MontoMinimo = disponibilidad.MontoMinimo,
                    Sucursal = new SucursalDto
                    {
                        Id = disponibilidad.Sucursal.Id,
                        Nombre = disponibilidad.Sucursal.Nombre,
                        Ciudad = disponibilidad.Sucursal.Ciudad
                    },
                    Producto = new ProductoDto
                    {
                        Id = disponibilidad.Producto.Id,
                        Nombre = disponibilidad.Producto.Nombre,
                        TipoProducto = disponibilidad.Producto.TipoProducto
                    }
                }
            };
        }
    }
} 