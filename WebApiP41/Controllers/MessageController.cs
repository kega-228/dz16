using Microsoft.AspNetCore.Mvc;
using WebApiP41.Services;
using WebApiP41.Models;

namespace WebApiP41.Controllers;

[ApiController]
[Route("api/messages")]
public class MessageController(MessageService messageService) : ControllerBase
{
    [HttpGet] // GET api/messages
    public async Task<IActionResult> GetAllMessages()
    {
        return Ok(await messageService.GetAllMessagesAsync());
    }

    [HttpPost] // POST/messages
    public async Task<IActionResult> createMessage([FromBody] Message message)
    {
        await messageService.AddMessageAsync(message);
        return Ok(message);
    }

    [HttpGet("{id:int}")] // GET api/messages/{id}
    public async Task<IActionResult> GetMessageById([FromRoute] int id)
    {
        var message = await messageService.GetMessageByIdAsync(id);
        return message is not null ? Ok(message) : NotFound();
    }

    [HttpGet("search")] // GET api/messages/search?from=test@test.com
    public async Task<IActionResult> SearchMessages([FromQuery] string? from)
    {
        var messages = from is not null
            ? await messageService.GetMessagesByFromAsync(from)
            : await messageService.GetAllMessagesAsync();
        return Ok(messages);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMessageById([FromRoute] int id)
    {
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
        var message = await messageService.UpdateMessageAsync(id, updatedMessage);
        return message is not null ? Ok(message) : NotFound();
    }
}