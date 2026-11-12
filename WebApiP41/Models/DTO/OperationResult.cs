namespace WebApiP41.Models.DTO;

/// <summary>
/// Результат виконання операції.
/// </summary>
public class OperationResult
{
    /// <summary>
    /// Ознака успішності операції.
    /// </summary>
    public bool Success{get;set;}

    /// <summary>
    /// Повідомлення про помилку, якщо операція завершилась невдало.
    /// </summary>
    public string ErrorMessage{get;set;} = string.Empty;
}