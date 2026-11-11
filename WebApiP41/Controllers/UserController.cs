using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Models;
using WebApiP41.Models.DTO;
using WebApiP41.Services;

namespace WebApiP41.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, UserService service)
    {
        var user = await service.AuthenticateAsync(request.Email, request.Password);
        if (user == null)
        {
            throw new Exception("Invalid credentials");
        }
        return Ok(mapper.Map<AuthResponce>(user));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers(UserService service)
    {
        var users = await service.GetAllUsersAsync();
        return Ok(mapper.Map<List<UserViewModel>>(users));
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetUserByName([FromRoute]string name, UserService service)
    {
        var user = await service.GetUserByName(name);
        return Ok(mapper.Map<UserViewModel>(user));
    }
}