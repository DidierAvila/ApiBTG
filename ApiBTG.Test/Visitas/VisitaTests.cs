using ApiBTG.Domain.Entities;
using FluentAssertions;

namespace ApiBTG.Test.Visitas
{
    public class VisitaTests
    {
        [Fact]
        public void Visita_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange
            var fechaVisita = DateTime.Now;

            // Act
            var visita = new Visita
            {
                Id = 1,
                IdSucursal = 5,
                IdCliente = 10,
                FechaVisita = fechaVisita,
                TipoAccion = "Consulta"
            };

            // Assert
            visita.Should().NotBeNull();
            visita.Id.Should().Be(1);
            visita.IdSucursal.Should().Be(5);
            visita.IdCliente.Should().Be(10);
            visita.FechaVisita.Should().Be(fechaVisita);
            visita.TipoAccion.Should().Be("Consulta");
        }

        [Fact]
        public void Visita_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var visita = new Visita
            {
                Id = 0,
                IdSucursal = 3,
                IdCliente = 7,
                FechaVisita = DateTime.Now,
                TipoAccion = "Solicitud"
            };

            // Assert
            visita.Id.Should().Be(0);
        }

        [Fact]
        public void Visita_ConFechaVisitaPasada_DebeTenerFechaCorrecta()
        {
            // Arrange
            var fechaPasada = DateTime.Now.AddDays(-5);

            // Act
            var visita = new Visita
            {
                Id = 7,
                IdSucursal = 11,
                IdCliente = 20,
                FechaVisita = fechaPasada,
                TipoAccion = "Consulta"
            };

            // Assert
            visita.FechaVisita.Should().Be(fechaPasada);
        }

        [Fact]
        public void Visita_ConSucursalAsignada_DebeTenerSucursalCorrecta()
        {
            // Arrange
            var sucursal = new Sucursal
            {
                Id = 5,
                Nombre = "Sucursal Centro",
                Ciudad = "Madrid"
            };

            // Act
            var visita = new Visita
            {
                Id = 12,
                IdSucursal = 5,
                IdCliente = 25,
                FechaVisita = DateTime.Now,
                TipoAccion = "Pago",
                Sucursal = sucursal
            };

            // Assert
            visita.Sucursal.Should().NotBeNull();
            visita.Sucursal.Should().Be(sucursal);
            visita.Sucursal.Id.Should().Be(5);
            visita.Sucursal.Nombre.Should().Be("Sucursal Centro");
        }

        [Fact]
        public void Visita_ConClienteAsignado_DebeTenerClienteCorrecto()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 10,
                Nombre = "Juan",
                Apellidos = "Pérez",
                Ciudad = "Barcelona",
                Monto = 1500.00m
            };

            // Act
            var visita = new Visita
            {
                Id = 13,
                IdSucursal = 8,
                IdCliente = 10,
                FechaVisita = DateTime.Now,
                TipoAccion = "Consulta",
                Cliente = cliente
            };

            // Assert
            visita.Cliente.Should().NotBeNull();
            visita.Cliente.Should().Be(cliente);
            visita.Cliente.Id.Should().Be(10);
            visita.Cliente.Nombre.Should().Be("Juan");
        }

        [Theory]
        [InlineData("Consulta")]
        [InlineData("Solicitud")]
        [InlineData("Pago")]
        public void Visita_ConTiposAccionValidos_DebeAceptarTipos(string tipoAccion)
        {
            // Arrange & Act
            var visita = new Visita
            {
                Id = 14,
                IdSucursal = 5,
                IdCliente = 10,
                FechaVisita = DateTime.Now,
                TipoAccion = tipoAccion
            };

            // Assert
            visita.TipoAccion.Should().Be(tipoAccion);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 10)]
        [InlineData(100, 200)]
        public void Visita_ConIdsValidos_DebeAceptarIds(int idSucursal, int idCliente)
        {
            // Arrange & Act
            var visita = new Visita
            {
                Id = 15,
                IdSucursal = idSucursal,
                IdCliente = idCliente,
                FechaVisita = DateTime.Now,
                TipoAccion = "Test"
            };

            // Assert
            visita.IdSucursal.Should().Be(idSucursal);
            visita.IdCliente.Should().Be(idCliente);
        }

        [Fact]
        public void Visita_ConMismaRelacionSucursalCliente_DebeMantenerRelacion()
        {
            // Arrange
            var sucursal = new Sucursal
            {
                Id = 15,
                Nombre = "Sucursal Norte",
                Ciudad = "Barcelona"
            };

            var cliente = new Cliente
            {
                Id = 25,
                Nombre = "María",
                Apellidos = "García",
                Ciudad = "Madrid",
                Monto = 2000.00m
            };

            // Act
            var visita = new Visita
            {
                Id = 16,
                IdSucursal = 15,
                IdCliente = 25,
                FechaVisita = DateTime.Now,
                TipoAccion = "Consulta",
                Sucursal = sucursal,
                Cliente = cliente
            };

            // Assert
            visita.IdSucursal.Should().Be(sucursal.Id);
            visita.IdCliente.Should().Be(cliente.Id);
            visita.Sucursal.Should().Be(sucursal);
            visita.Cliente.Should().Be(cliente);
        }

        [Fact]
        public void Visita_ConFechaVisitaConHora_DebeAceptarFechaConHora()
        {
            // Arrange
            var fechaConHora = new DateTime(2024, 1, 15, 14, 30, 45);

            // Act
            var visita = new Visita
            {
                Id = 21,
                IdSucursal = 5,
                IdCliente = 10,
                FechaVisita = fechaConHora,
                TipoAccion = "Cita Programada"
            };

            // Assert
            visita.FechaVisita.Should().Be(fechaConHora);
            visita.FechaVisita.Year.Should().Be(2024);
            visita.FechaVisita.Month.Should().Be(1);
            visita.FechaVisita.Day.Should().Be(15);
            visita.FechaVisita.Hour.Should().Be(14);
            visita.FechaVisita.Minute.Should().Be(30);
            visita.FechaVisita.Second.Should().Be(45);
        }
    }
} 