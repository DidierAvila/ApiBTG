using ApiBTG.Domain.Entities;
using FluentAssertions;

namespace ApiBTG.Test.Productos
{
    public class ProductoTests
    {
        [Fact]
        public void Producto_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Préstamo Personal",
                TipoProducto = "Préstamo"
            };

            // Assert
            producto.Should().NotBeNull();
            producto.Id.Should().Be(1);
            producto.Nombre.Should().Be("Préstamo Personal");
            producto.TipoProducto.Should().Be("Préstamo");
        }

        [Fact]
        public void Producto_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 0,
                Nombre = "Cuenta Corriente",
                TipoProducto = "Cuenta"
            };

            // Assert
            producto.Id.Should().Be(0);
        }

        [Fact]
        public void Producto_ConNombreVacio_DebeTenerNombreVacio()
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 2,
                Nombre = "",
                TipoProducto = "Depósito"
            };

            // Assert
            producto.Nombre.Should().BeEmpty();
        }

        [Fact]
        public void Producto_ConDisponibilidadesVacia_DebeTenerColeccionVacia()
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 4,
                Nombre = "Seguro de Vida",
                TipoProducto = "Seguro"
            };

            // Assert
            producto.Disponibilidades.Should().NotBeNull();
            producto.Disponibilidades.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Préstamo Personal")]
        [InlineData("Cuenta Corriente")]
        [InlineData("Tarjeta de Crédito")]
        public void Producto_ConNombresValidos_DebeAceptarNombres(string nombre)
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 5,
                Nombre = nombre,
                TipoProducto = "Financiero"
            };

            // Assert
            producto.Nombre.Should().Be(nombre);
        }

        [Theory]
        [InlineData("Préstamo")]
        [InlineData("Cuenta")]
        [InlineData("Tarjeta")]
        public void Producto_ConTiposProductoValidos_DebeAceptarTipos(string tipoProducto)
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 6,
                Nombre = "Producto Test",
                TipoProducto = tipoProducto
            };

            // Assert
            producto.TipoProducto.Should().Be(tipoProducto);
        }

        [Fact]
        public void Producto_ConDisponibilidadesAsignadas_DebeTenerDisponibilidadesCorrectas()
        {
            // Arrange
            var disponibilidad1 = new Disponibilidad
            {
                Id = 1,
                IdSucursal = 5,
                IdProducto = 7,
                MontoMinimo = 1000.00m
            };

            var disponibilidad2 = new Disponibilidad
            {
                Id = 2,
                IdSucursal = 8,
                IdProducto = 7,
                MontoMinimo = 2000.00m
            };

            // Act
            var producto = new Producto
            {
                Id = 7,
                Nombre = "Préstamo Empresarial",
                TipoProducto = "Préstamo",
                Disponibilidades = new List<Disponibilidad> { disponibilidad1, disponibilidad2 }
            };

            // Assert
            producto.Disponibilidades.Should().NotBeNull();
            producto.Disponibilidades.Should().HaveCount(2);
            producto.Disponibilidades.Should().Contain(disponibilidad1);
            producto.Disponibilidades.Should().Contain(disponibilidad2);
        }

        [Fact]
        public void Producto_ConNombreConCaracteresEspeciales_DebeAceptarNombre()
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 10,
                Nombre = "Préstamo & Crédito S.A.",
                TipoProducto = "Préstamo"
            };

            // Assert
            producto.Nombre.Should().Be("Préstamo & Crédito S.A.");
        }

        [Fact]
        public void Producto_ConNombreConNumeros_DebeAceptarNombre()
        {
            // Arrange & Act
            var producto = new Producto
            {
                Id = 12,
                Nombre = "Tarjeta Gold 2024",
                TipoProducto = "Tarjeta"
            };

            // Assert
            producto.Nombre.Should().Be("Tarjeta Gold 2024");
        }
    }
} 