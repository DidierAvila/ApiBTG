using ApiBTG.Application.Users;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiBTG.Test.Users
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedUser_WhenValidUserProvided()
        {
            // Arrange
            var createUser = new User
            {
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Password = "password123",
                Role = "usuario",
                NotificacionPreferida = "Email",
                Telefono = "+1234567890"
            };

            var createdUser = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Password = "password123",
                Role = "usuario",
                NotificacionPreferida = "Email",
                Telefono = "+1234567890"
            };

            _userRepositoryMock.Setup(x => x.ExistsByEmail(createUser.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.Create(createUser, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _userService.Create(createUser, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Email.Should().Be("juan.perez@example.com");
            _userRepositoryMock.Verify(x => x.Create(createUser, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var createUser = new User
            {
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Password = "password123",
                Role = "usuario"
            };

            _userRepositoryMock.Setup(x => x.ExistsByEmail(createUser.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userService.Create(createUser, CancellationToken.None));
        }

        [Fact]
        public async Task Create_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var createUser = new User
            {
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "",
                Password = "password123",
                Role = "usuario"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _userService.Create(createUser, CancellationToken.None));
        }

        [Fact]
        public async Task Create_ShouldThrowException_WhenPasswordIsEmpty()
        {
            // Arrange
            var createUser = new User
            {
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Password = "",
                Role = "usuario"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _userService.Create(createUser, CancellationToken.None));
        }

        [Fact]
        public async Task Get_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Role = "usuario"
            };

            _userRepositoryMock.Setup(x => x.GetByID(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.Get(1, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Email.Should().Be("juan.perez@example.com");
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetByID(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.Get(999, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenInvalidIdProvided()
        {
            // Act
            var result = await _userService.Get(0, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Juan", LastName = "Pérez", Email = "juan@example.com" },
                new User { Id = 2, FirstName = "María", LastName = "García", Email = "maria@example.com" }
            };

            _userRepositoryMock.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAll(CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(u => u.Id == 1);
            result.Should().Contain(u => u.Id == 2);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedUser_WhenUserExists()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Role = "usuario"
            };

            var updateRequest = new User
            {
                Id = 1,
                FirstName = "Juan Carlos",
                LastName = "Pérez López",
                Email = "juan.perez@example.com",
                Role = "admin"
            };

            _userRepositoryMock.Setup(x => x.GetByID(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(x => x.GetByEmail(updateRequest.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            // Act
            var result = await _userService.Update(updateRequest, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Juan Carlos");
            result.LastName.Should().Be("Pérez López");
            result.Role.Should().Be("admin");
            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var updateRequest = new User
            {
                Id = 999,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Role = "usuario"
            };

            _userRepositoryMock.Setup(x => x.GetByID(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _userService.Update(updateRequest, CancellationToken.None));
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com"
            };

            _userRepositoryMock.Setup(x => x.GetByID(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            // Act
            var result = await _userService.Delete(1, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _userRepositoryMock.Verify(x => x.Delete(1, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetByID(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.Delete(999, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _userRepositoryMock.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenInvalidIdProvided()
        {
            // Act
            var result = await _userService.Delete(0, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistsByEmail_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.ExistsByEmail("juan.perez@example.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.ExistsByEmail("juan.perez@example.com", CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsByEmail_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.ExistsByEmail("nonexistent@example.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _userService.ExistsByEmail("nonexistent@example.com", CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistsByEmail_ShouldReturnFalse_WhenEmailIsEmpty()
        {
            // Act
            var result = await _userService.ExistsByEmail("", CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com"
            };

            _userRepositoryMock.Setup(x => x.GetByEmail("juan.perez@example.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetByEmail("juan.perez@example.com", CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("juan.perez@example.com");
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetByEmail("nonexistent@example.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userService.GetByEmail("nonexistent@example.com", CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnNull_WhenEmailIsEmpty()
        {
            // Act
            var result = await _userService.GetByEmail("", CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByRole_ShouldReturnUsers_WhenUsersExist()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Juan", LastName = "Pérez", Role = "admin" },
                new User { Id = 2, FirstName = "María", LastName = "García", Role = "admin" }
            };

            _userRepositoryMock.Setup(x => x.GetByRole("admin", It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetByRole("admin", CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(u => u.Role == "admin");
        }

        [Fact]
        public async Task GetByRole_ShouldReturnEmptyList_WhenNoUsersWithRole()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetByRole("nonexistent", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User>());

            // Act
            var result = await _userService.GetByRole("nonexistent", CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByRole_ShouldReturnEmptyList_WhenRoleIsEmpty()
        {
            // Act
            var result = await _userService.GetByRole("", CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Juan", "juan.perez@example.com", "password123", "usuario")]
        [InlineData("María", "maria.garcia@example.com", "securepass456", "admin")]
        public async Task Create_ShouldHandleValidData_WithTheory(string firstName, string email, string password, string role)
        {
            // Arrange
            var createUser = new User
            {
                FirstName = firstName,
                LastName = "Test",
                Email = email,
                Password = password,
                Role = role
            };

            var createdUser = new User
            {
                Id = 1,
                FirstName = firstName,
                LastName = "Test",
                Email = email,
                Password = password,
                Role = role
            };

            _userRepositoryMock.Setup(x => x.ExistsByEmail(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.Create(createUser, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _userService.Create(createUser, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(firstName);
            result.Email.Should().Be(email);
            result.Role.Should().Be(role);
        }
    }
} 