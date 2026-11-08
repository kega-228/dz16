using Microsoft.AspNetCore.Mvc;
using WebApiP41.Services;
using WebApiP41.Models;

namespace WebApiP41.Controllers;

[ApiController]
[Route("api/messages")]
public class MessageController(MessageService messageService) : ControllerBase
{
    private User? user => HttpContext.Items["User"] as User;
    [HttpGet] // GET api/messages
    public async Task<IActionResult> GetAllMessages()
    {
        if (user is null) return Unauthorized();
        return Ok(await messageService.GetAllMessagesAsync());
    }

    [HttpPost] // POST/messages
    public async Task<IActionResult> createMessage([FromBody] Message message)
    {
        if (user is null) return Unauthorized();
        await messageService.AddMessageAsync(message);
        return Ok(message);
    }

    [HttpGet("{id:int}")] // GET api/messages/{id}
    public async Task<IActionResult> GetMessageById([FromRoute] int id)
    {
        if (user is null) return Unauthorized();
        var message = await messageService.GetMessageByIdAsync(id);
        return message is not null ? Ok(message) : NotFound();
    }

    [HttpGet("search")] // GET api/messages/search?from=test@test.com
    public async Task<IActionResult> SearchMessages([FromQuery] string? from)
    {
        if (user is null) return Unauthorized();
        var messages = from is not null
            ? await messageService.GetMessagesByFromAsync(from)
            : await messageService.GetAllMessagesAsync();
        return Ok(messages);
    }

    [HttpDelete("{id:int}")]
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

    [HttpPut("{id:int}")]
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