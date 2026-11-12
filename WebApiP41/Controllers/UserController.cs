using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Models;
using WebApiP41.Models.DTO;
using WebApiP41.Services;

namespace WebApiP41.Controllers;

/// <summary>
/// Керування користувачами: реєстрація, аутентифікація та перегляд списку.
/// </summary>
[ApiController]
[Route("api/users")]
[Tags("User management")]
public class UserController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Реєстрація нового користувача.
    /// </summary>
    /// <param name="request">Дані для реєстрації (ім'я, email, пароль)</param>
    /// <param name="service">Сервіс користувачів</param>
    /// <returns>Статус операції</returns>
    /// <response code="200">Користувача успішно зареєстровано</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, UserService service)
    {
        var newUser = mapper.Map<User>(request);
        try
        {
            var user = await service.RegisterUserAsync(newUser);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Аутентифікація користувача та отримання JWT токена.
    /// </summary>
    /// <param name="request">Данні для входу (email, пароль)</param>
    /// <param name="service">Сервіс користувачів</param>
    /// <returns>Дані авторизації з токеном</returns>
    /// <response code="200">Успішний вхід</response>
    /// <response code="401">Невірні облікові дані</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, UserService service)
    {
        var user = await service.AuthenticateAsync(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }
        return Ok(mapper.Map<AuthResponce>(user));
    }

    /// <summary>
    /// Отримати список усіх зареєстрованих користувачів.
    /// </summary>
    /// <param name="service">Сервіс користувачів</param>
    /// <returns>Список користувачів</returns>
    /// <response code="200">Список успішно отримано</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers(UserService service)
    {
        var users = await service.GetAllUsersAsync();
        return Ok(mapper.Map<List<UserViewModel>>(users));
    }
    
    /// <summary>
    /// Отримати дані користувача за ім'ям.
    /// </summary>
    /// <param name="name">Ім'я користувача</param>
    /// <param name="service">Сервіс користувачів</param>
    /// <returns>Дані користувача</returns>
    /// <response code="200">Користувача знайдено</response>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserByName([FromRoute]string name, UserService service)
    {
        var user = await service.GetUserByName(name);
        return Ok(mapper.Map<UserViewModel>(user));
    }
}