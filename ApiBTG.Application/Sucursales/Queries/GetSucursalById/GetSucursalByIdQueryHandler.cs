using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Sucursales.Queries.GetSucursalById
{
    public class GetSucursalByIdQueryHandler : IRequestHandler<GetSucursalByIdQuery, SucursalDto?>
    {
        private readonly ISucursalRepository _sucursalRepository;

        public GetSucursalByIdQueryHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<SucursalDto?> Handle(GetSucursalByIdQuery request, CancellationToken cancellationToken)
        {
            var sucursal = await _sucursalRepository.GetByID(request.Id, cancellationToken);

            if (sucursal == null)
                return null;

            return new SucursalDto
            {
                Id = sucursal.Id,
                Nombre = sucursal.Nombre,
                Ciudad = sucursal.Ciudad
            };
        }
    }
} 