using Microsoft.AspNetCore.Mvc;
using WebApiP41.Services;
using WebApiP41.Models;

namespace WebApiP41.Controllers;

/// <summary>
/// Керування повідомленнями: перегляд, пошук, створення, редагування та видалення.
/// </summary>
/// <remarks>
/// Усі методи потребують авторизації: заголовок <c>Authorization: Bearer {token}</c>,
/// токен повертається методом <c>POST /api/users/login</c>.
/// Редагувати та видаляти повідомлення може лише його автор (<c>FromAddress</c> збігається з email користувача).
/// </remarks>
[ApiController]
[Route("api/messages")]
[Tags("Message management")]
[Produces("application/json")]
public class MessageController(MessageService messageService) : ControllerBase
{
    private User? user => HttpContext.Items["User"] as User;
    
    /// <summary>
    /// Отримати всі повідомлення
    /// </summary>
    /// <returns>Список усіх повідомлень</returns>
    /// <response code="200">Список повідомлень успішно отримано</response>
    /// <response code="401">Токен відсутній або недійсний</response>
    [HttpGet] // GET api/messages
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMessages()
    {
        if (user is null) return Unauthorized();
        return Ok(await messageService.GetAllMessagesAsync());
    }

    /// <summary>
    /// Створити нове повідомлення
    /// </summary>
    /// <param name="message">Дані повідомлення: email відправника, текст та дата й час</param>
    /// <returns>Створене повідомлення</returns>
    /// <response code="200">Повідомлення створено</response>
    /// <response code="400">Дані запиту не пройшли валідацію</response>
    /// <response code="401">Токен відсутній або недійсний</response>
    [HttpPost] // POST api/messages
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> createMessage([FromBody] Message message)
    {
        if (user is null) return Unauthorized();
        await messageService.AddMessageAsync(message);
        return Ok(message);
    }

    /// <summary>
    /// Отримати повідомлення за ідентифікатором
    /// </summary>
    /// <param name="id">Унікальний ідентифікатор повідомлення</param>
    /// <returns>Знайдене повідомлення</returns>
    /// <response code="200">Повідомлення знайдено</response>
    /// <response code="401">Токен відсутній або недійсний</response>
    /// <response code="404">Повідомлення з таким <paramref name="id"/> не знайдено</response>
    [HttpGet("{id:int}")] // GET api/messages/{id}
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMessageById([FromRoute] int id)
    {
        if (user is null) return Unauthorized();
        var message = await messageService.GetMessageByIdAsync(id);
        return message is not null ? Ok(message) : NotFound();
    }

    /// <summary>
    /// Пошук повідомлень за адресою відправника
    /// </summary>
    /// <remarks>
    /// Приклад: <c>GET /api/messages/search?from=test@test.com</c>.
    /// Якщо параметр <c>from</c> не вказано, повертаються всі повідомлення.
    /// </remarks>
    /// <param name="from">Email відправника (необовʼязковий параметр запиту)</param>
    /// <returns>Список повідомлень, що відповідають умові пошуку</returns>
    /// <response code="200">Пошук виконано</response>
    /// <response code="401">Токен відсутній або недійсний</response>
    [HttpGet("search")] // GET api/messages/search?from=test@test.com
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchMessages([FromQuery] string? from)
    {
        if (user is null) return Unauthorized();
        var messages = from is not null
            ? await messageService.GetMessagesByFromAsync(from)
            : await messageService.GetAllMessagesAsync();
        return Ok(messages);
    }

    /// <summary>
    /// Видалити повідомлення
    /// </summary>
    /// <remarks>Видалити повідомлення може лише його автор.</remarks>
    /// <param name="id">Унікальний ідентифікатор повідомлення</param>
    /// <response code="200">Повідомлення видалено</response>
    /// <response code="401">Токен відсутній або недійсний</response>
    /// <response code="403">Повідомлення належить іншому користувачу</response>
    /// <response code="404">Повідомлення з таким <paramref name="id"/> не знайдено</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMessageById([FromRoute] int id)
    {
        if (user is null) return Unauthorized();
        var message = await messageService.GetMessageByIdAsync(id);
        if (message is null) return NotFound();
        if (!user.Email.Equals(message.FromAddress, StringComparison.OrdinalIgnoreCase)) return Forbid();
        var result = await messageService.DeleteMessageAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    /// <summary>
    /// Оновити повідомлення
    /// </summary>
    /// <remarks>Редагувати повідомлення може лише його автор.</remarks>
    /// <param name="id">Унікальний ідентифікатор повідомлення, яке потрібно оновити</param>
    /// <param name="updatedMessage">Нові дані повідомлення</param>
    /// <returns>Оновлене повідомлення</returns>
    /// <response code="200">Повідомлення оновлено</response>
    /// <response code="400">Дані запиту не пройшли валідацію</response>
    /// <response code="401">Токен відсутній або недійсний</response>
    /// <response code="403">Повідомлення належить іншому користувачу</response>
    /// <response code="404">Повідомлення з таким <paramref name="id"/> не знайдено</response>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutMessageById([FromRoute] int id, [FromBody] Message updatedMessage)
    {
        if (user is null) return Unauthorized();
        var messages = await messageService.GetMessageByIdAsync(id);
        if (messages is null) return NotFound();
        if (!user.Email.Equals(messages.FromAddress, StringComparison.OrdinalIgnoreCase)) return Forbid();
        var message = await messageService.UpdateMessageAsync(id, updatedMessage);
        return message is not null ? Ok(message) : NotFound();
    }
}