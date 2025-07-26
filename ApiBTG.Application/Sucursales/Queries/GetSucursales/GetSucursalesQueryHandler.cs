using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Sucursales.Queries.GetSucursales
{
    public class GetSucursalesQueryHandler : IRequestHandler<GetSucursalesQuery, IEnumerable<SucursalDto>>
    {
        private readonly ISucursalRepository _sucursalRepository;

        public GetSucursalesQueryHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<IEnumerable<SucursalDto>> Handle(GetSucursalesQuery request, CancellationToken cancellationToken)
        {
            var sucursales = await _sucursalRepository.GetAll(cancellationToken);

            return sucursales.Select(s => new SucursalDto
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Ciudad = s.Ciudad
            });
        }
    }
} 