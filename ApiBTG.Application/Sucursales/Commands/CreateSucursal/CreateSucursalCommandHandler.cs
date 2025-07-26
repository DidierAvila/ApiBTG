using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Sucursales.Commands.CreateSucursal
{
    public class CreateSucursalCommandHandler : IRequestHandler<CreateSucursalCommand, SucursalDto>
    {
        private readonly ISucursalRepository _sucursalRepository;

        public CreateSucursalCommandHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<SucursalDto> Handle(CreateSucursalCommand request, CancellationToken cancellationToken)
        {
            var sucursal = new Sucursal
            {
                Nombre = request.Nombre,
                Ciudad = request.Ciudad
            };

            var createdSucursal = await _sucursalRepository.Create(sucursal, cancellationToken);

            return new SucursalDto
            {
                Id = createdSucursal.Id,
                Nombre = createdSucursal.Nombre,
                Ciudad = createdSucursal.Ciudad
            };
        }
    }
} 