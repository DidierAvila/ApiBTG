using ApiBTG.Domain.Entities;
using FluentAssertions;

namespace ApiBTG.Test.Inscripciones
{
    public class InscripcionTests
    {
        [Fact]
        public void Inscripcion_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var inscripcion = new Inscripcion
            {
                Id = 1,
                IdCliente = 5,
                IdDisponibilidad = 10
            };

            // Assert
            inscripcion.Should().NotBeNull();
            inscripcion.Id.Should().Be(1);
            inscripcion.IdCliente.Should().Be(5);
            inscripcion.IdDisponibilidad.Should().Be(10);
        }

        [Fact]
        public void Inscripcion_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var inscripcion = new Inscripcion
            {
                Id = 0,
                IdCliente = 3,
                IdDisponibilidad = 7
            };

            // Assert
            inscripcion.Id.Should().Be(0);
        }

        [Fact]
        public void Inscripcion_ConClienteAsignado_DebeTenerClienteCorrecto()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 5,
                Nombre = "Juan",
                Apellidos = "Pérez",
                Ciudad = "Madrid",
                Monto = 1500.00m
            };

            // Act
            var inscripcion = new Inscripcion
            {
                Id = 8,
                IdCliente = 5,
                IdDisponibilidad = 25,
                Cliente = cliente
            };

            // Assert
            inscripcion.Cliente.Should().NotBeNull();
            inscripcion.Cliente.Should().Be(cliente);
            inscripcion.Cliente.Id.Should().Be(5);
            inscripcion.Cliente.Nombre.Should().Be("Juan");
        }

        [Fact]
        public void Inscripcion_ConDisponibilidadAsignada_DebeTenerDisponibilidadCorrecta()
        {
            // Arrange
            var disponibilidad = new Disponibilidad
            {
                Id = 10,
                IdSucursal = 3,
                IdProducto = 7,
                MontoMinimo = 1000.00m
            };

            // Act
            var inscripcion = new Inscripcion
            {
                Id = 9,
                IdCliente = 8,
                IdDisponibilidad = 10,
                Disponibilidad = disponibilidad
            };

            // Assert
            inscripcion.Disponibilidad.Should().NotBeNull();
            inscripcion.Disponibilidad.Should().Be(disponibilidad);
            inscripcion.Disponibilidad.Id.Should().Be(10);
            inscripcion.Disponibilidad.MontoMinimo.Should().Be(1000.00m);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 10)]
        [InlineData(100, 200)]
        public void Inscripcion_ConIdsValidos_DebeAceptarIds(int idCliente, int idDisponibilidad)
        {
            // Arrange & Act
            var inscripcion = new Inscripcion
            {
                Id = 10,
                IdCliente = idCliente,
                IdDisponibilidad = idDisponibilidad
            };

            // Assert
            inscripcion.IdCliente.Should().Be(idCliente);
            inscripcion.IdDisponibilidad.Should().Be(idDisponibilidad);
        }

        [Fact]
        public void Inscripcion_ConMismaRelacionClienteDisponibilidad_DebeMantenerRelacion()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 15,
                Nombre = "María",
                Apellidos = "García",
                Ciudad = "Barcelona",
                Monto = 2000.00m
            };

            var disponibilidad = new Disponibilidad
            {
                Id = 25,
                IdSucursal = 5,
                IdProducto = 12,
                MontoMinimo = 1500.00m
            };

            // Act
            var inscripcion = new Inscripcion
            {
                Id = 11,
                IdCliente = 15,
                IdDisponibilidad = 25,
                Cliente = cliente,
                Disponibilidad = disponibilidad
            };

            // Assert
            inscripcion.IdCliente.Should().Be(cliente.Id);
            inscripcion.IdDisponibilidad.Should().Be(disponibilidad.Id);
            inscripcion.Cliente.Should().Be(cliente);
            inscripcion.Disponibilidad.Should().Be(disponibilidad);
        }

        [Fact]
        public void Inscripcion_ConIdsNegativos_DebeAceptarIdsNegativos()
        {
            // Arrange & Act
            var inscripcion = new Inscripcion
            {
                Id = 12,
                IdCliente = -5,
                IdDisponibilidad = -10
            };

            // Assert
            inscripcion.IdCliente.Should().Be(-5);
            inscripcion.IdDisponibilidad.Should().Be(-10);
        }

        [Fact]
        public void Inscripcion_ConIdMaximo_DebeAceptarIdMaximo()
        {
            // Arrange & Act
            var inscripcion = new Inscripcion
            {
                Id = int.MaxValue,
                IdCliente = int.MaxValue,
                IdDisponibilidad = int.MaxValue
            };

            // Assert
            inscripcion.Id.Should().Be(int.MaxValue);
            inscripcion.IdCliente.Should().Be(int.MaxValue);
            inscripcion.IdDisponibilidad.Should().Be(int.MaxValue);
        }

        [Fact]
        public void Inscripcion_ConIdMinimo_DebeAceptarIdMinimo()
        {
            // Arrange & Act
            var inscripcion = new Inscripcion
            {
                Id = int.MinValue,
                IdCliente = int.MinValue,
                IdDisponibilidad = int.MinValue
            };

            // Assert
            inscripcion.Id.Should().Be(int.MinValue);
            inscripcion.IdCliente.Should().Be(int.MinValue);
            inscripcion.IdDisponibilidad.Should().Be(int.MinValue);
        }
    }
} 