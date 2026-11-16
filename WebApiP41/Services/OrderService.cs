using Microsoft.EntityFrameworkCore;
using WebApiP41.Models;

namespace WebApiP41.Services;

public class OrderService(ApiDbContext dbContext)
{
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await dbContext.Orders.ToListAsync();
    }
    
    public async Task<Order> AddOrderAsync(Order neworder)
    {
        var maxOrderNumber = await dbContext.Orders.MaxAsync(p => (int?)p.OrderNumber) ?? 0;
        neworder.OrderNumber = maxOrderNumber + 1;
        neworder.Date = DateTime.Now;
        neworder.IsComplete = false;
        dbContext.Orders.Add(neworder);
        await dbContext.SaveChangesAsync();
        return neworder;
    }

    public async Task<Order?> UpdateOrderQuantity(int id, int quantity)
    {
        var orders = await dbContext.Orders.ToListAsync();
        var order = orders.FirstOrDefault(p => p.Id == id);
        if (order == null)
        {
            return null;
        }
        order.Quantity = quantity;
        await dbContext.SaveChangesAsync();
        return order;
    }
    public async Task<Order?> UpdateOrderStatusAsync(int id, bool isComplete)
    {
        var orders = await dbContext.Orders.ToListAsync();
        var order = orders.FirstOrDefault(p => p.Id == id);
        if (order == null)
        {
            return null;
        }
        order.IsComplete = isComplete;
        await dbContext.SaveChangesAsync();
        return order;
    } 
}