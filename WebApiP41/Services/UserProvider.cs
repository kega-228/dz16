using WebApiP41.Models;

namespace WebApiP41.Services;

public class UserProvider : IDataProvider<User>
{
    private readonly string dataFile;
    
    public UserProvider(string dataFile)
    {
        this.dataFile = dataFile;
    }
    public async Task<ICollection<User>> GetAllAsync()
    {
        if (!File.Exists(dataFile))
        {
            return new List<User>();
        }
        var json = await File.ReadAllTextAsync(dataFile);
        return System.Text.Json.JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    public Task SaveAsync(ICollection<User> items)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(items);
        return File.WriteAllTextAsync(dataFile, json);
    }
}