using ApiBTG.Application.Clientes.Queries.GetClientes;
using ApiBTG.Application.Security;
using ApiBTG.Application.Users;
using ApiBTG.Application.Visitas.Queries.GetVisitas;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;

namespace ApiBTG.Extenciones
{
    public static class Extencion
    {
        public static IServiceCollection AddApiBTGExtention(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
            services.AddScoped<IRepositoryBase<Token>, RepositoryBase<Token>>();

            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetClientesQuery).Assembly));
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetVisitasQueryHandler).Assembly));

            // Register Repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ISucursalRepository, SucursalRepository>();
            services.AddScoped<IInscripcionRepository, InscripcionRepository>();
            services.AddScoped<IDisponibilidadRepository, DisponibilidadRepository>();
            services.AddScoped<IVisitaRepository, VisitaRepository>();

            return services;
        }
    }
}
