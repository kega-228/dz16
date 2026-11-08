using System.ComponentModel.DataAnnotations;

namespace WebApiP41.Models.DTO;

public class LoginRequest
{
    [Required(ErrorMessage = "Please enter your email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [MinLength(5, ErrorMessage = "Email address must be at least 5 characters long")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please enter your password")]
    public string Password { get; set; } = string.Empty;
}