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
            var user = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@email.com",
                Password = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email",
                Telefono = "+34612345678"
            };

            // Assert
            user.Should().NotBeNull();
            user.Id.Should().Be(1);
            user.FirstName.Should().Be("Juan");
            user.LastName.Should().Be("Pérez");
            user.Email.Should().Be("juan.perez@email.com");
            user.Password.Should().Be("password123");
            user.Role.Should().Be("Cliente");
            user.NotificacionPreferida.Should().Be("Email");
            user.Telefono.Should().Be("+34612345678");
        }

        [Fact]
        public void User_ConIdCero_DebeTenerIdCero()
        {
            // Arrange & Act
            var user = new User
            {
                Id = 0,
                FirstName = "María",
                LastName = "García",
                Email = "maria.garcia@email.com",
                Password = "password456",
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
            var user = new User
            {
                Id = 2,
                FirstName = "",
                LastName = "Martínez",
                Email = "test@email.com",
                Password = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.FirstName.Should().BeEmpty();
        }

        [Fact]
        public void User_ConTelefonoNulo_DebeTenerTelefonoNulo()
        {
            // Arrange & Act
            var user = new User
            {
                Id = 8,
                FirstName = "Diego",
                LastName = "Moreno",
                Email = "diego@email.com",
                Password = "password789",
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
            var user = new User
            {
                Id = 9,
                FirstName = firstName,
                LastName = "Test",
                Email = "test@email.com",
                Password = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.FirstName.Should().Be(firstName);
        }

        [Theory]
        [InlineData("test@email.com")]
        [InlineData("user.name@domain.com")]
        [InlineData("admin@company.org")]
        public void User_ConEmailsValidos_DebeAceptarEmails(string email)
        {
            // Arrange & Act
            var user = new User
            {
                Id = 11,
                FirstName = "Test",
                LastName = "User",
                Email = email,
                Password = "password123",
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
            var user = new User
            {
                Id = 13,
                FirstName = "Test",
                LastName = "User",
                Email = "test@email.com",
                Password = "password123",
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
            var user = new User
            {
                Id = 23,
                FirstName = "Test",
                LastName = "User",
                Email = "test@email.com",
                Password = "password123",
                Role = "Cliente"
            };

            // Assert
            user.NotificacionPreferida.Should().Be("Email");
        }

        [Fact]
        public void User_ConCaracteresEspecialesEnNombres_DebeAceptarCaracteresEspeciales()
        {
            // Arrange & Act
            var user = new User
            {
                Id = 24,
                FirstName = "José María",
                LastName = "García-López",
                Email = "jose.maria@email.com",
                Password = "password123",
                Role = "Cliente",
                NotificacionPreferida = "Email"
            };

            // Assert
            user.FirstName.Should().Be("José María");
            user.LastName.Should().Be("García-López");
        }
    }
} 