using Microsoft.EntityFrameworkCore;
using WebApiP41.Models;

namespace WebApiP41.Services;

public class MessageService(ApiDbContext dbContext)
{

    public async Task<List<Message>> GetAllMessagesAsync()
    {
        var messages = await dbContext.Messages.ToListAsync();
        return messages.ToList();
    }

    public async Task<Message?> GetMessageByIdAsync(int id)
    {
        var message = await dbContext.Messages.FirstOrDefaultAsync(p => p.Id == id);
        return message;
    }

    public async Task<List<Message>?> GetMessagesByFromAsync(string from)
    {
        var messages = await dbContext.Messages.ToListAsync();
        return messages.Where(p => p.FromAddress.Equals(from, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public async Task<Message> AddMessageAsync(Message newMessage)
    {
        var messages = await dbContext.Messages.ToListAsync();
        dbContext.Messages.Add(newMessage);
        await dbContext.SaveChangesAsync();
        return newMessage;
    }

    public async Task<Message?> UpdateMessageAsync(int id, Message updatedMessage)
    {
        var message = await dbContext.Messages.FirstOrDefaultAsync(p => p.Id == id);
        if (message == null)
        {
            return null;
        }
        message.FromAddress = updatedMessage.FromAddress;
        message.Text = updatedMessage.Text;
        message.DateTime = updatedMessage.DateTime;
        await dbContext.SaveChangesAsync();
        return message;
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        var messages = await dbContext.Messages.ToListAsync();
        var message = messages.FirstOrDefault(p => p.Id == id);
        if (message == null)
        {
            return false;
        }
        messages.Remove(message);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
