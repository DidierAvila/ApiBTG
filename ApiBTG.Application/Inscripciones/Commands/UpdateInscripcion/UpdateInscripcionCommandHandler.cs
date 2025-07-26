using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.UpdateInscripcion
{
    public class UpdateInscripcionCommandHandler : IRequestHandler<UpdateInscripcionCommand, InscripcionDto>
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public UpdateInscripcionCommandHandler(IInscripcionRepository inscripcionRepository, IClienteRepository clienteRepository, IDisponibilidadRepository disponibilidadRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<InscripcionDto> Handle(UpdateInscripcionCommand request, CancellationToken cancellationToken)
        {
            var inscripcion = await _inscripcionRepository.GetByID(request.Id, cancellationToken);
            if (inscripcion == null)
                throw new KeyNotFoundException($"Inscripción con ID {request.Id} no encontrada");

            // Validar que no exista otra inscripción del mismo cliente para la nueva disponibilidad
            var exists = await _inscripcionRepository.ExistsInscripcionAsync(request.IdCliente, request.IdDisponibilidad, cancellationToken);
            if (exists && (inscripcion.IdCliente != request.IdCliente || inscripcion.IdDisponibilidad != request.IdDisponibilidad))
                throw new InvalidOperationException($"Ya existe una inscripción para el cliente {request.IdCliente} en la disponibilidad {request.IdDisponibilidad}");

            // Validar que la disponibilidad existe
            var disponibilidad = await _disponibilidadRepository.GetByID(request.IdDisponibilidad, cancellationToken);
            if (disponibilidad == null)
                throw new KeyNotFoundException($"Disponibilidad con ID {request.IdDisponibilidad} no encontrada");

            inscripcion.IdCliente = request.IdCliente;
            inscripcion.IdDisponibilidad = request.IdDisponibilidad;

            await _inscripcionRepository.Update(inscripcion, cancellationToken);

            var cliente = await _clienteRepository.GetByID(inscripcion.IdCliente, cancellationToken);

            return new InscripcionDto
            {
                Id = inscripcion.Id,
                IdCliente = inscripcion.IdCliente,
                IdDisponibilidad = inscripcion.IdDisponibilidad,
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