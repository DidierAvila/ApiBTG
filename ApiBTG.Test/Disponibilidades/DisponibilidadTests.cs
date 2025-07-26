using ApiBTG.Domain.Entities;
using FluentAssertions;

namespace ApiBTG.Test.Disponibilidades
{
    public class DisponibilidadTests
    {
        [Fact]
        public void Disponibilidad_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var disponibilidad = new Disponibilidad
            {
                Id = 1,
                IdSucursal = 5,
                IdProducto = 10,
                MontoMinimo = 1500.00m
            };

            // Assert
            disponibilidad.Should().NotBeNull();
            disponibilidad.Id.Should().Be(1);
            disponibilidad.IdSucursal.Should().Be(5);
            disponibilidad.IdProducto.Should().Be(10);
            disponibilidad.MontoMinimo.Should().Be(1500.00m);
        }

        [Fact]
        public void Disponibilidad_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var disponibilidad = new Disponibilidad
            {
                Id = 0,
                IdSucursal = 3,
                IdProducto = 7,
                MontoMinimo = 1000.00m
            };

            // Assert
            disponibilidad.Id.Should().Be(0);
        }

        [Fact]
        public void Disponibilidad_ConMontoMinimoNegativo_DebeTenerMontoMinimoNegativo()
        {
            // Arrange & Act
            var disponibilidad = new Disponibilidad
            {
                Id = 7,
                IdSucursal = 11,
                IdProducto = 20,
                MontoMinimo = -500.00m
            };

            // Assert
            disponibilidad.MontoMinimo.Should().Be(-500.00m);
        }

        [Fact]
        public void Disponibilidad_ConSucursalAsignada_DebeTenerSucursalCorrecta()
        {
            // Arrange
            var sucursal = new Sucursal
            {
                Id = 5,
                Nombre = "Sucursal Centro",
                Ciudad = "Madrid"
            };

            // Act
            var disponibilidad = new Disponibilidad
            {
                Id = 10,
                IdSucursal = 5,
                IdProducto = 25,
                MontoMinimo = 2000.00m,
                Sucursal = sucursal
            };

            // Assert
            disponibilidad.Sucursal.Should().NotBeNull();
            disponibilidad.Sucursal.Should().Be(sucursal);
            disponibilidad.Sucursal.Id.Should().Be(5);
            disponibilidad.Sucursal.Nombre.Should().Be("Sucursal Centro");
        }

        [Fact]
        public void Disponibilidad_ConProductoAsignado_DebeTenerProductoCorrecto()
        {
            // Arrange
            var producto = new Producto
            {
                Id = 10,
                Nombre = "Préstamo Personal",
                TipoProducto = "Préstamo"
            };

            // Act
            var disponibilidad = new Disponibilidad
            {
                Id = 11,
                IdSucursal = 8,
                IdProducto = 10,
                MontoMinimo = 1800.00m,
                Producto = producto
            };

            // Assert
            disponibilidad.Producto.Should().NotBeNull();
            disponibilidad.Producto.Should().Be(producto);
            disponibilidad.Producto.Id.Should().Be(10);
            disponibilidad.Producto.Nombre.Should().Be("Préstamo Personal");
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 10)]
        [InlineData(100, 200)]
        public void Disponibilidad_ConIdsValidos_DebeAceptarIds(int idSucursal, int idProducto)
        {
            // Arrange & Act
            var disponibilidad = new Disponibilidad
            {
                Id = 12,
                IdSucursal = idSucursal,
                IdProducto = idProducto,
                MontoMinimo = 1500.00m
            };

            // Assert
            disponibilidad.IdSucursal.Should().Be(idSucursal);
            disponibilidad.IdProducto.Should().Be(idProducto);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(100.50)]
        [InlineData(1000.00)]
        public void Disponibilidad_ConMontosMinimosValidos_DebeAceptarMontos(decimal montoMinimo)
        {
            // Arrange & Act
            var disponibilidad = new Disponibilidad
            {
                Id = 13,
                IdSucursal = 5,
                IdProducto = 10,
                MontoMinimo = montoMinimo
            };

            // Assert
            disponibilidad.MontoMinimo.Should().Be(montoMinimo);
        }

        [Fact]
        public void Disponibilidad_ConMismaRelacionSucursalProducto_DebeMantenerRelacion()
        {
            // Arrange
            var sucursal = new Sucursal
            {
                Id = 15,
                Nombre = "Sucursal Norte",
                Ciudad = "Barcelona"
            };

            var producto = new Producto
            {
                Id = 25,
                Nombre = "Tarjeta de Crédito",
                TipoProducto = "Tarjeta"
            };

            // Act
            var disponibilidad = new Disponibilidad
            {
                Id = 14,
                IdSucursal = 15,
                IdProducto = 25,
                MontoMinimo = 2500.00m,
                Sucursal = sucursal,
                Producto = producto
            };

            // Assert
            disponibilidad.IdSucursal.Should().Be(sucursal.Id);
            disponibilidad.IdProducto.Should().Be(producto.Id);
            disponibilidad.Sucursal.Should().Be(sucursal);
            disponibilidad.Producto.Should().Be(producto);
        }

        [Fact]
        public void Disponibilidad_ConMontoMinimoConDecimales_DebeAceptarMonto()
        {
            // Arrange & Act
            var disponibilidad = new Disponibilidad
            {
                Id = 16,
                IdSucursal = 5,
                IdProducto = 10,
                MontoMinimo = 1234.56m
            };

            // Assert
            disponibilidad.MontoMinimo.Should().Be(1234.56m);
        }
    }
} 