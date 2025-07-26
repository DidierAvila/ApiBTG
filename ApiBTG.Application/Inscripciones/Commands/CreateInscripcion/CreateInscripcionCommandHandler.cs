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
        private readonly IProductoRepository _productoRepository;
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public CreateInscripcionCommandHandler(
            IInscripcionRepository inscripcionRepository,
            IClienteRepository clienteRepository,
            IProductoRepository productoRepository,
            IDisponibilidadRepository disponibilidadRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<InscripcionDto> Handle(CreateInscripcionCommand request, CancellationToken cancellationToken)
        {
            // Verificar que el cliente existe
            var cliente = await _clienteRepository.GetByID(request.IdCliente, cancellationToken);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {request.IdCliente} no encontrado");
            }

            // Verificar que el producto existe
            var producto = await _productoRepository.GetByID(request.IdProducto, cancellationToken);
            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto con ID {request.IdProducto} no encontrado");
            }

            // Verificar que no existe ya una inscripción
            var exists = await _inscripcionRepository.ExistsInscripcionAsync(request.IdProducto, request.IdCliente, cancellationToken);
            if (exists)
            {
                throw new InvalidOperationException($"Ya existe una inscripción para el cliente {request.IdCliente} en el producto {request.IdProducto}");
            }

            // Buscar la disponibilidad del producto para obtener el monto mínimo
            var disponibilidades = await _disponibilidadRepository.GetDisponibilidadesByProductoAsync(request.IdProducto, cancellationToken);
            if (!disponibilidades.Any())
            {
                throw new InvalidOperationException($"No hay disponibilidad para el producto {producto.Nombre}");
            }

            // Usar el monto mínimo de la primera disponibilidad encontrada
            var montoMinimo = disponibilidades.First().MontoMinimo;

            // Verificar que el cliente cumple con el monto mínimo
            if (cliente.Monto < montoMinimo)
            {
                throw new InvalidOperationException($"El cliente {cliente.Nombre} no tiene saldo disponible para vincularse al fondo {producto.Nombre}");
            }

            var inscripcion = new Inscripcion
            {
                IdProducto = request.IdProducto,
                IdCliente = request.IdCliente
            };

            var createdInscripcion = await _inscripcionRepository.Create(inscripcion, cancellationToken);

            // Actualizar el monto del cliente
            cliente.Monto -= montoMinimo;
            await _clienteRepository.Update(cliente, cancellationToken);

            return new InscripcionDto
            {
                Id = createdInscripcion.Id,
                IdProducto = createdInscripcion.IdProducto,
                IdCliente = createdInscripcion.IdCliente,
                Cliente = new ClienteDto
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellidos = cliente.Apellidos,
                    Ciudad = cliente.Ciudad,
                    Monto = cliente.Monto
                },
                Producto = new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    TipoProducto = producto.TipoProducto
                }
            };
        }
    }
} 