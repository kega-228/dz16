using WebApiP41.Models;

namespace WebApiP41.Services;

public class MessageService
{
    private readonly IDataProvider<Message> dataProvider;

    public MessageService(IDataProvider<Message> dataFile)
    {
        this.dataProvider = dataFile;
    }

    public async Task<List<Message>> GetAllMessagesAsync()
    {
        var messages = await dataProvider.GetAllAsync();
        return messages.ToList();
    }

    public async Task<Message?> GetMessageByIdAsync(int id)
    {
        var messages = await dataProvider.GetAllAsync();
        return messages.FirstOrDefault(p => p.Id == id);
    }

    public async Task<List<Message>?> GetMessagesByFromAsync(string from)
    {
        var messages = await dataProvider.GetAllAsync();
        return messages.Where(p => p.FromAddress.Equals(from, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public async Task<Message> AddMessageAsync(Message newMessage)
    {
        var messages = await dataProvider.GetAllAsync();
        newMessage.Id = messages.Any() ? messages.Max(p => p.Id) + 1 : 1;
        messages.Add(newMessage);
        await dataProvider.SaveAsync(messages);
        return newMessage;
    }

    public async Task<Message?> UpdateMessageAsync(int id, Message updatedMessage)
    {
        var messages = await dataProvider.GetAllAsync();
        var message = messages.FirstOrDefault(p => p.Id == id);
        if (message == null)
        {
            return null;
        }
        message.FromAddress = updatedMessage.FromAddress;
        message.Text = updatedMessage.Text;
        message.DateTime = updatedMessage.DateTime;
        await dataProvider.SaveAsync(messages);
        return message;
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        var messages = await dataProvider.GetAllAsync();
        var message = messages.FirstOrDefault(p => p.Id == id);
        if (message == null)
        {
            return false;
        }
        messages.Remove(message);
        await dataProvider.SaveAsync(messages);
        return true;
    }
}
