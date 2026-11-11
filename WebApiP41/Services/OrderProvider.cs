using WebApiP41.Models;

namespace WebApiP41.Services;

public class OrderProvider : IDataProvider<Order>
{
    private readonly string _dataFile;
    public OrderProvider(string dataFile)
    {
        _dataFile = dataFile;
    }
    
    public async Task<ICollection<Order>> GetAllAsync()
    {
        if (!File.Exists(_dataFile))
        {
            return new List<Order>();
        }
        var json = await File.ReadAllTextAsync(_dataFile);
        return System.Text.Json.JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
    }

    public async Task SaveAsync(ICollection<Order> items)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(_dataFile, json);
    }
}