using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Sucursales.Commands.UpdateSucursal
{
    public class UpdateSucursalCommandHandler : IRequestHandler<UpdateSucursalCommand, SucursalDto>
    {
        private readonly ISucursalRepository _sucursalRepository;

        public UpdateSucursalCommandHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<SucursalDto> Handle(UpdateSucursalCommand request, CancellationToken cancellationToken)
        {
            var sucursal = await _sucursalRepository.GetByID(request.Id, cancellationToken);

            if (sucursal == null)
            {
                throw new KeyNotFoundException($"Sucursal con ID {request.Id} no encontrada");
            }

            sucursal.Nombre = request.Nombre;
            sucursal.Ciudad = request.Ciudad;

            await _sucursalRepository.Update(sucursal, cancellationToken);

            return new SucursalDto
            {
                Id = sucursal.Id,
                Nombre = sucursal.Nombre,
                Ciudad = sucursal.Ciudad
            };
        }
    }
} 