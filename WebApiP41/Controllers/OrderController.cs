using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Models;
using WebApiP41.Models.DTO;
using WebApiP41.Services;

namespace WebApiP41.Controllers;

[ApiController]
[Route("api/orders")]
[Tags("Order management")]
public class OrderController(IMapper mapper, OrderService service) : ControllerBase
{
    /// <summary>
    /// Get all orders
    /// </summary>
    /// <returns>Orders</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<OrderViewModel>),StatusCodes.Status200OK)]
    public async Task<ICollection<OrderViewModel>> ObserveOrders()
    {
        var orders = await service.GetAllOrdersAsync();
        return orders.Select(order => mapper.Map<OrderViewModel>(order)).ToList();
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrder([FromBody] AddRequest request)
    {
        var newOrder = mapper.Map<Order>(request);
        var addedOrder = await service.AddOrderAsync(newOrder);
        return Ok(mapper.Map<OrderViewModel>(addedOrder));
    }

    [HttpPut("{id}/quantity")]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrderQuantity(int id, [FromBody] int quantity)
    {
        var updatedOrder = await service.UpdateOrderQuantity(id, quantity);
        if (updatedOrder == null)
        {
            return NotFound();
        }
        return Ok(mapper.Map<OrderViewModel>(updatedOrder));
    }

    [HttpPut("{id}/status")]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] bool isComplete)
    {
        var updatedOrder = await service.UpdateOrderStatusAsync(id, isComplete);
        if (updatedOrder == null)
        {
            return NotFound();
        }
        return Ok(mapper.Map<OrderViewModel>(updatedOrder));
    }
    
}