using WebApiP41.Models;

namespace WebApiP41.Services;

public class OrderService(IDataProvider<Order> orderProvider)
{
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return (await orderProvider.GetAllAsync()).ToList();
    }
    
    public async Task<Order> AddOrderAsync(Order neworder)
    {
        var orders = await orderProvider.GetAllAsync();
        neworder.Id = orders.Any() ? orders.Max(p => p.Id) + 1 : 1;
        var maxOrderNumber = orders.Any() ? orders.Max(p => p.OrderNumber) : 0;
        neworder.OrderNumber = maxOrderNumber + 1;
        neworder.Date = DateTime.Now;
        neworder.IsComplete = false;
        orders.Add(neworder);
        await orderProvider.SaveAsync(orders);
        return neworder;
    }

    public async Task<Order?> UpdateOrderQuantity(int id, int quantity)
    {
        var orders = await orderProvider.GetAllAsync();
        var order = orders.FirstOrDefault(p => p.Id == id);
        if (order == null)
        {
            return null;
        }
        order.Quantity = quantity;
        await orderProvider.SaveAsync(orders);
        return order;
    }
    public async Task<Order?> UpdateOrderStatusAsync(int id, bool isComplete)
    {
        var orders = await orderProvider.GetAllAsync();
        var order = orders.FirstOrDefault(p => p.Id == id);
        if (order == null)
        {
            return null;
        }
        order.IsComplete = isComplete;
        await orderProvider.SaveAsync(orders);
        return order;
    } 
}