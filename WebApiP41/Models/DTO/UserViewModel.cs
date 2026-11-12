namespace WebApiP41.Models.DTO;

/// <summary>
/// Публічні дані користувача (без пароля та токена).
/// </summary>
public class UserViewModel
{
    /// <summary>
    /// Імʼя користувача.
    /// </summary>
    /// <example>Іван</example>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Email користувача.
    /// </summary>
    /// <example>ivan@example.com</example>
    public string Email { get; set; } = string.Empty;
}