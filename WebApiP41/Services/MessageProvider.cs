using WebApiP41.Models;
namespace WebApiP41.Services;

public class MessageProvider : IDataProvider<Message>
{
    private readonly string dataFile;
    
    public MessageProvider(string dataFile)
    {
        this.dataFile = dataFile;
    }
    
    public async Task<ICollection<Message>> GetAllAsync()
    {
        if (!File.Exists(dataFile))
        {
            return new List<Message>();
        }
        var json = await File.ReadAllTextAsync(dataFile);
        return System.Text.Json.JsonSerializer.Deserialize<List<Message>>(json) ?? new List<Message>();
    }

    public async Task SaveAsync(ICollection<Message> items)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(dataFile, json);
    }
}