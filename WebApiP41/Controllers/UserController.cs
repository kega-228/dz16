using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Models;
using WebApiP41.Models.DTO;
using WebApiP41.Services;

namespace WebApiP41.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, UserService service)
    {
        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        try
        {
            var user = await service.RegisterUserAsync(newUser);
            return Ok(new AuthResponce{Name = user.Name, Token = user.Token});
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
        return Ok(new AuthResponce{Name = user.Name, Token = user.Token});
    }
}