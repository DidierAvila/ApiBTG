using ApiBTG.Application.Users;
using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;


        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<ApiResponseDto<ICollection<User>>>> GetUsers()
        {
            try
            {
                ICollection<User> users = await _userService.GetAll(cancellationToken: CancellationToken.None);
                return Ok(ApiResponseDto<ICollection<User>>.SuccessResult(users, "Usuarios obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios");
                return StatusCode(500, ApiResponseDto<IEnumerable<UserDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<ApiResponseDto<User>>> GetUser(int id)
        {
            try
            {
                User? user = await _userService.Get(id, cancellationToken: CancellationToken.None);
                if (user == null)
                {
                    return NotFound(ApiResponseDto<UserDto>.ErrorResult($"Usuario con ID {id} no encontrado"));
                }
                return Ok(ApiResponseDto<User>.SuccessResult(user, "Usuario obtenido exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<UserDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult<ApiResponseDto<User>>> CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<UserDto>.ErrorResult("Datos de entrada inválidos", errors));
                }
                var user = await _userService.Create(
                    new User() { FirstName = createUserDto.FirstName, LastName = createUserDto.LastName, Email = createUserDto.Email, Role = createUserDto.Role, Password = createUserDto.Password },
                    cancellationToken: CancellationToken.None);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id },
                    ApiResponseDto<User>.SuccessResult(user, "Usuario creado exitosamente"));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear usuario");
                return BadRequest(ApiResponseDto<UserDto>.ErrorResult("Error de validación", ex.Errors.Select(e => e.ErrorMessage).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                return StatusCode(500, ApiResponseDto<UserDto>.ErrorResult("Error interno del servidor"));
            }
        }
    }
}