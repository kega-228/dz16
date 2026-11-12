namespace WebApiP41.Models.DTO;

/// <summary>
/// Відповідь на успішну авторизацію, що містить токен та ім'я користувача.
/// </summary>
public class AuthResponce
{
    /// <summary>
    /// JWT токен для доступу до захищених ресурсів.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Ім'я авторизованого користувача.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}