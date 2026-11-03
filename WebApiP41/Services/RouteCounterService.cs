using System.Text.Json;
using WebApiP41.Models;

namespace WebApiP41.Services;

public class RouteCounterService
{
    private readonly string _dataFilePath;
    private static readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);

    public RouteCounterService(string dataFile)
    {
        _dataFilePath = dataFile;
    }

    private async Task<List<RouteCounter>> LoadRouteCountersAsync()
    {
        await _fileLock.WaitAsync();
        try
        {
            if (!File.Exists(_dataFilePath))
            {
                return new List<RouteCounter>();
            }

            var json = await File.ReadAllTextAsync(_dataFilePath);
            return JsonSerializer.Deserialize<List<RouteCounter>>(json) ?? new List<RouteCounter>();
        }
        finally
        {
            _fileLock.Release();
        }
    }

    private async Task SaveRouteCountersAsync(List<RouteCounter> routeCounters)
    {
        await _fileLock.WaitAsync();
        try
        {
            var json = JsonSerializer.Serialize(routeCounters);
            await File.WriteAllTextAsync(_dataFilePath, json);
        }
        finally
        {
            _fileLock.Release();
        }
        
    }

    public async Task IncrementRouteCounterAsync(string route, string method)
    {
        var routeCounters = await LoadRouteCountersAsync();
        var counter = routeCounters.FirstOrDefault(p => p.Route == route && p.Method == method);
        if (counter == null)
        {
            counter = new RouteCounter { Route = route, Method = method, Count = 1 };
            routeCounters.Add(counter);
        }
        else
        {
            counter.Count++;
        }
        await SaveRouteCountersAsync(routeCounters);
    }
}
