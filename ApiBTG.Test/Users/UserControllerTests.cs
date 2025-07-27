using ApiBTG.Application.Users.Commands.CreateUser;
using ApiBTG.Application.Users.Commands.DeleteUser;
using ApiBTG.Application.Users.Commands.UpdateUser;
using ApiBTG.Application.Users.Queries.GetUserById;
using ApiBTG.Application.Users.Queries.GetUsers;
using ApiBTG.Controllers;
using ApiBTG.Domain.Dtos;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace ApiBTG.Test.Users
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOkResult_WhenUsersExist()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto
                {
                    Id = 1,
                    FirstName = "Juan",
                    LastName = "Pérez",
                    Email = "juan.perez@example.com",
                    Role = "usuario",
                    NotificacionPreferida = "Email",
                    Telefono = "+1234567890"
                }
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ApiResponseDto<IEnumerable<UserDto>>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetUser_ShouldReturnOkResult_WhenUserExists()
        {
            // Arrange
            var user = new UserDto
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Role = "usuario",
                NotificacionPreferida = "Email",
                Telefono = "+1234567890"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetUser(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserDto?)null);

            // Act
            var result = await _controller.GetUser(999);

            // Assert
            var notFoundResult = result.Result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var response = notFoundResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedResult_WhenUserIsValid()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                FirstName = "María",
                LastName = "García",
                Email = "maria.garcia@example.com",
                Password = "password123",
                Role = "usuario",
                NotificacionPreferida = "Email",
                Telefono = "+1234567890"
            };

            var createdUser = new UserDto
            {
                Id = 1,
                FirstName = "María",
                LastName = "García",
                Email = "maria.garcia@example.com",
                Role = "usuario",
                NotificacionPreferida = "Email",
                Telefono = "+1234567890"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _controller.CreateUser(createUserDto);

            // Assert
            var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var response = createdResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.Id.Should().Be(1);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnOkResult_WhenUserExists()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto
            {
                FirstName = "María",
                LastName = "García López",
                Email = "maria.garcia@example.com",
                Role = "admin",
                NotificacionPreferida = "SMS",
                Telefono = "+1234567890"
            };

            var updatedUser = new UserDto
            {
                Id = 1,
                FirstName = "María",
                LastName = "García López",
                Email = "maria.garcia@example.com",
                Role = "admin",
                NotificacionPreferida = "SMS",
                Telefono = "+1234567890"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedUser);

            // Act
            var result = await _controller.UpdateUser(1, updateUserDto);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.Role.Should().Be("admin");
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOkResult_WhenUserExists()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ApiResponseDto<bool>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(999);

            // Assert
            var notFoundResult = result.Result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var response = notFoundResult.Value.Should().BeOfType<ApiResponseDto<bool>>().Subject;
            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task GetCurrentUserProfile_ShouldReturnOkResult_WhenUserIsAuthenticated()
        {
            // Arrange
            var user = new UserDto
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Role = "usuario",
                NotificacionPreferida = "Email",
                Telefono = "+1234567890"
            };

            var claims = new List<Claim>
            {
                new Claim("UserId", "1")
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"))
                }
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetCurrentUserProfile();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetCurrentUserProfile_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            };

            // Act
            var result = await _controller.GetCurrentUserProfile();

            // Assert
            var unauthorizedResult = result.Result.Should().BeOfType<UnauthorizedObjectResult>().Subject;
            var response = unauthorizedResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeFalse();
        }

        [Theory]
        [InlineData("Juan", "Pérez", "juan.perez@example.com", "password123", "usuario", "Email", "+1234567890")]
        [InlineData("María", "García", "maria.garcia@example.com", "securepass456", "admin", "SMS", "+0987654321")]
        public async Task CreateUser_ShouldHandleValidData_WithTheory(string firstName, string lastName, string email, 
            string password, string role, string notificacionPreferida, string telefono)
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = role,
                NotificacionPreferida = notificacionPreferida,
                Telefono = telefono
            };

            var createdUser = new UserDto
            {
                Id = 1,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Role = role,
                NotificacionPreferida = notificacionPreferida,
                Telefono = telefono
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _controller.CreateUser(createUserDto);

            // Assert
            var createdResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var response = createdResult.Value.Should().BeOfType<ApiResponseDto<UserDto>>().Subject;
            response.Success.Should().BeTrue();
            response.Data.FirstName.Should().Be(firstName);
            response.Data.LastName.Should().Be(lastName);
            response.Data.Email.Should().Be(email);
        }
    }
} 