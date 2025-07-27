using ApiBTG.Application.Clientes.Queries.GetClientes;
using ApiBTG.Application.Security;
using ApiBTG.Application.Users;
using ApiBTG.Application.Visitas.Queries.GetVisitas;
using ApiBTG.Application.Common.Interfaces;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using ApiBTG.Application.Notificaciones;

namespace ApiBTG.Extenciones
{
    public static class Extencion
    {
        public static IServiceCollection AddApiBTGExtention(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepositoryBase<Usuario>, RepositoryBase<Usuario>>();
            services.AddScoped<IRepositoryBase<Token>, RepositoryBase<Token>>();
            services.AddScoped<IRepositoryBase<Usuario>, RepositoryBase<Usuario>>();
            
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetClientesQuery).Assembly));
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetVisitasQueryHandler).Assembly));

            // Register Repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ISucursalRepository, SucursalRepository>();
            services.AddScoped<IInscripcionRepository, InscripcionRepository>();
            services.AddScoped<IDisponibilidadRepository, DisponibilidadRepository>();
            services.AddScoped<IVisitaRepository, VisitaRepository>();

            // Register Notification Services
            services.AddScoped<EmailNotificationService>();
            services.AddScoped<SmsNotificationService>();
            services.AddScoped<NotificacionCommandHandler>();   

            return services;
        }
    }
}
