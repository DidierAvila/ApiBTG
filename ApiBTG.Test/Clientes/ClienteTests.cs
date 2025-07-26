using ApiBTG.Domain.Entities;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace ApiBTG.Test.Clientes
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "Juan",
                Apellidos = "Pérez",
                Ciudad = "Madrid",
                Monto = 1500.50m,
                UsuarioId = 1
            };

            // Assert
            cliente.Should().NotBeNull();
            cliente.Nombre.Should().Be("Juan");
            cliente.Apellidos.Should().Be("Pérez");
            cliente.Ciudad.Should().Be("Madrid");
            cliente.Monto.Should().Be(1500.50m);
            cliente.UsuarioId.Should().Be(1);
        }

        [Fact]
        public void Cliente_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Id = 0,
                Nombre = "María",
                Apellidos = "García",
                Ciudad = "Barcelona",
                Monto = 2000.00m
            };

            // Assert
            cliente.Id.Should().Be(0);
        }

        [Fact]
        public void Cliente_ConNombreVacio_DebeTenerNombreVacio()
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "",
                Apellidos = "Martínez",
                Ciudad = "Sevilla",
                Monto = 1000.00m
            };

            // Assert
            cliente.Nombre.Should().BeEmpty();
        }

        [Fact]
        public void Cliente_ConMontoNegativo_DebeTenerMontoNegativo()
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "Roberto",
                Apellidos = "Jiménez",
                Ciudad = "Zaragoza",
                Monto = -500.00m
            };

            // Assert
            cliente.Monto.Should().Be(-500.00m);
        }

        [Fact]
        public void Cliente_ConUsuarioIdNulo_DebeTenerUsuarioIdNulo()
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "Carmen",
                Apellidos = "Ruiz",
                Ciudad = "Alicante",
                Monto = 1200.00m,
                UsuarioId = null
            };

            // Assert
            cliente.UsuarioId.Should().BeNull();
        }

        [Fact]
        public void Cliente_ConInscripcionesVacia_DebeTenerColeccionVacia()
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "Elena",
                Apellidos = "Vázquez",
                Ciudad = "Valladolid",
                Monto = 1600.00m
            };

            // Assert
            cliente.Inscripciones.Should().NotBeNull();
            cliente.Inscripciones.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Juan Carlos")]
        [InlineData("María José")]
        [InlineData("José María")]
        public void Cliente_ConNombresValidos_DebeAceptarNombres(string nombre)
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = nombre,
                Apellidos = "González",
                Ciudad = "Pamplona",
                Monto = 1400.00m
            };

            // Assert
            cliente.Nombre.Should().Be(nombre);
        }

        [Theory]
        [InlineData("Pérez García")]
        [InlineData("López Martínez")]
        [InlineData("García Fernández")]
        public void Cliente_ConApellidosValidos_DebeAceptarApellidos(string apellidos)
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "Miguel",
                Apellidos = apellidos,
                Ciudad = "Oviedo",
                Monto = 2100.00m
            };

            // Assert
            cliente.Apellidos.Should().Be(apellidos);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(100.50)]
        [InlineData(1000.00)]
        public void Cliente_ConMontosValidos_DebeAceptarMontos(decimal monto)
        {
            // Arrange & Act
            var cliente = new Cliente
            {
                Nombre = "Alejandro",
                Apellidos = "Castro",
                Ciudad = "Santander",
                Monto = monto
            };

            // Assert
            cliente.Monto.Should().Be(monto);
        }
    }
} 