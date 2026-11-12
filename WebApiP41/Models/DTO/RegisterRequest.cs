using System.ComponentModel.DataAnnotations;

namespace WebApiP41.Models.DTO;

/// <summary>
/// Запит на реєстрацію нового користувача.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Ім'я користувача.
    /// </summary>
    [Required(ErrorMessage = "Please enter your name")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    [MaxLength(50, ErrorMessage = "Name must be at most 50 characters long")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email користувача для реєстрації.
    /// </summary>
    [Required(ErrorMessage = "Please enter your email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [MinLength(5, ErrorMessage = "Email address must be at least 5 characters long")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль користувача.
    /// </summary>
    [Required(ErrorMessage = "Please enter your password")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;
}