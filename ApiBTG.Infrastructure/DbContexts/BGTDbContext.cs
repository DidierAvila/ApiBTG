using ApiBTG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.DbContexts
{
    public partial class BGTDbContext : DbContext
    {
        public BGTDbContext(DbContextOptions<BGTDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Sucursal> Sucursales { get; set; }
        public virtual DbSet<Inscripcion> Inscripciones { get; set; }
        public virtual DbSet<Disponibilidad> Disponibilidades { get; set; }
        public virtual DbSet<Visita> Visitas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys using Id property (auto-increment)
            modelBuilder.Entity<Token>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<User>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Cliente>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Producto>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Sucursal>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Inscripcion>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Disponibilidad>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Visita>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Visita>()
                .Property(v => v.TipoAccion)
                .HasMaxLength(50)
                .IsRequired();

            // Configure unique constraints for business logic
            modelBuilder.Entity<Inscripcion>()
                .HasIndex(i => new { i.IdCliente, i.IdDisponibilidad })
                .IsUnique();

            modelBuilder.Entity<Disponibilidad>()
                .HasIndex(d => new { d.IdSucursal, d.IdProducto })
                .IsUnique();

            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
