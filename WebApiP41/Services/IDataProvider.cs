namespace WebApiP41.Services;

public interface IDataProvider<T>
{
    public Task<ICollection<T>> GetAllAsync();
    public Task SaveAsync(ICollection<T> items);
}