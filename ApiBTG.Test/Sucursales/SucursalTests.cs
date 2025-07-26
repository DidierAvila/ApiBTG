using ApiBTG.Domain.Entities;
using FluentAssertions;

namespace ApiBTG.Test.Sucursales
{
    public class SucursalTests
    {
        [Fact]
        public void Sucursal_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 1,
                Nombre = "Sucursal Centro",
                Ciudad = "Madrid"
            };

            // Assert
            sucursal.Should().NotBeNull();
            sucursal.Id.Should().Be(1);
            sucursal.Nombre.Should().Be("Sucursal Centro");
            sucursal.Ciudad.Should().Be("Madrid");
        }

        [Fact]
        public void Sucursal_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 0,
                Nombre = "Sucursal Norte",
                Ciudad = "Barcelona"
            };

            // Assert
            sucursal.Id.Should().Be(0);
        }

        [Fact]
        public void Sucursal_ConNombreVacio_DebeTenerNombreVacio()
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 2,
                Nombre = "",
                Ciudad = "Sevilla"
            };

            // Assert
            sucursal.Nombre.Should().BeEmpty();
        }

        [Fact]
        public void Sucursal_ConDisponibilidadesVacia_DebeTenerColeccionVacia()
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 4,
                Nombre = "Sucursal Oeste",
                Ciudad = "Bilbao"
            };

            // Assert
            sucursal.Disponibilidades.Should().NotBeNull();
            sucursal.Disponibilidades.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Sucursal Centro")]
        [InlineData("Sucursal Norte")]
        [InlineData("Sucursal Sur")]
        public void Sucursal_ConNombresValidos_DebeAceptarNombres(string nombre)
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 6,
                Nombre = nombre,
                Ciudad = "Test City"
            };

            // Assert
            sucursal.Nombre.Should().Be(nombre);
        }

        [Theory]
        [InlineData("Madrid")]
        [InlineData("Barcelona")]
        [InlineData("Valencia")]
        public void Sucursal_ConCiudadesValidas_DebeAceptarCiudades(string ciudad)
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 7,
                Nombre = "Test Sucursal",
                Ciudad = ciudad
            };

            // Assert
            sucursal.Ciudad.Should().Be(ciudad);
        }

        [Fact]
        public void Sucursal_ConDisponibilidadesAsignadas_DebeTenerDisponibilidadesCorrectas()
        {
            // Arrange
            var disponibilidad1 = new Disponibilidad
            {
                Id = 1,
                IdSucursal = 8,
                IdProducto = 5,
                MontoMinimo = 1000.00m
            };

            var disponibilidad2 = new Disponibilidad
            {
                Id = 2,
                IdSucursal = 8,
                IdProducto = 10,
                MontoMinimo = 2000.00m
            };

            // Act
            var sucursal = new Sucursal
            {
                Id = 8,
                Nombre = "Sucursal Premium",
                Ciudad = "Madrid",
                Disponibilidades = new List<Disponibilidad> { disponibilidad1, disponibilidad2 }
            };

            // Assert
            sucursal.Disponibilidades.Should().NotBeNull();
            sucursal.Disponibilidades.Should().HaveCount(2);
            sucursal.Disponibilidades.Should().Contain(disponibilidad1);
            sucursal.Disponibilidades.Should().Contain(disponibilidad2);
        }

        [Fact]
        public void Sucursal_ConNombreConCaracteresEspeciales_DebeAceptarNombre()
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 12,
                Nombre = "Sucursal & Centro Comercial",
                Ciudad = "Madrid"
            };

            // Assert
            sucursal.Nombre.Should().Be("Sucursal & Centro Comercial");
        }

        [Fact]
        public void Sucursal_ConNombreConNumeros_DebeAceptarNombre()
        {
            // Arrange & Act
            var sucursal = new Sucursal
            {
                Id = 14,
                Nombre = "Sucursal 2024",
                Ciudad = "Barcelona"
            };

            // Assert
            sucursal.Nombre.Should().Be("Sucursal 2024");
        }
    }
} 