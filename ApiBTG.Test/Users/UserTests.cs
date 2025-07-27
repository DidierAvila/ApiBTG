using ApiBTG.Domain.Entities;
using FluentAssertions;

namespace ApiBTG.Test.Users
{
    public class UserTests
    {
        [Fact]
        public void User_ConDatosValidos_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan.perez@email.com",
                Clave = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email",
                Telefono = "+34612345678"
            };

            // Assert
            user.Should().NotBeNull();
            user.Id.Should().Be(1);
            user.Nombre.Should().Be("Juan");
            user.Apellido.Should().Be("Pérez");
            user.Email.Should().Be("juan.perez@email.com");
            user.Clave.Should().Be("password123");
            user.Role.Should().Be("Cliente");
            user.NotificacionPreferida.Should().Be("Email");
            user.Telefono.Should().Be("+34612345678");
        }

        [Fact]
        public void User_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 0,
                Nombre = "María",
                Apellido = "García",
                Email = "maria.garcia@email.com",
                Clave = "password456",
                Role = "Admin",
                NotificacionPreferida = "SMS",
                Telefono = "+34687654321"
            };

            // Assert
            user.Id.Should().Be(0);
        }

        [Fact]
        public void User_ConFirstNameVacio_DebeTenerFirstNameVacio()
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 2,
                Nombre = "",
                Apellido = "Martínez",
                Email = "test@email.com",
                Clave = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.Nombre.Should().BeEmpty();
        }

        [Fact]
        public void User_ConTelefonoNulo_DebeTenerTelefonoNulo()
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 8,
                Nombre = "Diego",
                Apellido = "Moreno",
                Email = "diego@email.com",
                Clave = "password789",
                Role = "Cliente",
                NotificacionPreferida = "Email",
                Telefono = null
            };

            // Assert
            user.Telefono.Should().BeNull();
        }

        [Theory]
        [InlineData("Juan")]
        [InlineData("María")]
        [InlineData("Carlos")]
        public void User_ConFirstNamesValidos_DebeAceptarFirstNames(string firstName)
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 9,
                Nombre = firstName,
                Apellido = "Test",
                Email = "test@email.com",
                Clave = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.Nombre.Should().Be(firstName);
        }

        [Theory]
        [InlineData("test@email.com")]
        [InlineData("user.name@domain.com")]
        [InlineData("admin@company.org")]
        public void User_ConEmailsValidos_DebeAceptarEmails(string email)
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 11,
                Nombre = "Test",
                Apellido = "User",
                Email = email,
                Clave = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.Email.Should().Be(email);
        }

        [Theory]
        [InlineData("Cliente")]
        [InlineData("Admin")]
        [InlineData("Empleado")]
        public void User_ConRolesValidos_DebeAceptarRoles(string role)
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 13,
                Nombre = "Test",
                Apellido = "User",
                Email = "test@email.com",
                Clave = "password123",
                Role = role,
                NotificacionPreferida = "Email"
            };

            // Assert
            user.Role.Should().Be(role);
        }

        [Fact]
        public void User_ConNotificacionPreferidaPorDefecto_DebeTenerEmailPorDefecto()
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 23,
                Nombre = "Test",
                Apellido = "User",
                Email = "test@email.com",
                Clave = "password123",
                Role = "Cliente"
            };

            // Assert
            user.NotificacionPreferida.Should().Be("Email");
        }

        [Fact]
        public void User_ConCaracteresEspecialesEnNombres_DebeAceptarCaracteresEspeciales()
        {
            // Arrange & Act
            var user = new Usuario
            {
                Id = 24,
                Nombre = "José María",
                Apellido = "García-López",
                Email = "jose.maria@email.com",
                Clave = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.Nombre.Should().Be("José María");
            user.Apellido.Should().Be("García-López");
        }
    }
} 