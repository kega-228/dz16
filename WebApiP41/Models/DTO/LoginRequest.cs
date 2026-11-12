using System.ComponentModel.DataAnnotations;

namespace WebApiP41.Models.DTO;

/// <summary>
/// Запит на вхід у систему (аутентифікація).
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Email користувача.
    /// </summary>
    [Required(ErrorMessage = "Please enter your email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [MinLength(5, ErrorMessage = "Email address must be at least 5 characters long")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль користувача.
    /// </summary>
    [Required(ErrorMessage = "Please enter your password")]
    public string Password { get; set; } = string.Empty;
}