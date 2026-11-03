namespace WebApiP41.Models;

public class RouteCounter
{
    public string Method { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public int Count { get; set; }
}