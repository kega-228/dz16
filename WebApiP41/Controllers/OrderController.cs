using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Models;
using WebApiP41.Models.DTO;
using WebApiP41.Services;

namespace WebApiP41.Controllers;

/// <summary>
/// Керування замовленнями: перегляд списку, створення, зміна кількості та статусу виконання.
/// </summary>
/// <remarks>
/// Маршрути <c>/api/orders</c> не потребують токена авторизації (див. <c>CheckUsersMiddleware</c>).
/// </remarks>
[ApiController]
[Route("api/orders")]
[Tags("Order management")]
public class OrderController(IMapper mapper, OrderService service) : ControllerBase
{
    /// <summary>
    /// Отримати список усіх замовлень.
    /// </summary>
    /// <returns>Колекція замовлень у форматі <see cref="OrderViewModel"/></returns>
    /// <response code="200">Список замовлень успішно отримано</response>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<OrderViewModel>),StatusCodes.Status200OK)]
    public async Task<ICollection<OrderViewModel>> ObserveOrders()
    {
        var orders = await service.GetAllOrdersAsync();
        return orders.Select(order => mapper.Map<OrderViewModel>(order)).ToList();
    }

    /// <summary>
    /// Створити нове замовлення.
    /// </summary>
    /// <param name="request">Дані для створення замовлення (кількість, ціна, коментар)</param>
    /// <returns>Створене замовлення</returns>
    /// <response code="200">Замовлення успішно створено</response>
    /// <response code="400">Помилка валідації даних</response>
    [HttpPost]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrder([FromBody] AddRequest request)
    {
        var newOrder = mapper.Map<Order>(request);
        var addedOrder = await service.AddOrderAsync(newOrder);
        return Ok(mapper.Map<OrderViewModel>(addedOrder));
    }

    /// <summary>
    /// Змінити кількість товару в замовленні
    /// </summary>
    /// <remarks>
    /// У тілі запиту передається просте число, а не JSON-обʼєкт. Приклад: <c>5</c>
    /// </remarks>
    /// <param name="id">Унікальний ідентифікатор замовлення</param>
    /// <param name="quantity">Нова кількість товару (ціле число)</param>
    /// <returns>Оновлене замовлення</returns>
    /// <response code="200">Кількість оновлено</response>
    /// <response code="400">Некоректне значення в тілі запиту</response>
    /// <response code="404">Замовлення з таким <paramref name="id"/> не знайдено</response>
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

    /// <summary>
    /// Змінити статус виконання замовлення
    /// </summary>
    /// <remarks>
    /// У тілі запиту передається <c>true</c> або <c>false</c>.
    /// </remarks>
    /// <param name="id">Унікальний ідентифікатор замовлення</param>
    /// <param name="isComplete"><c>true</c>, якщо замовлення виконано; <c>false</c>, якщо ні</param>
    /// <returns>Оновлене замовлення</returns>
    /// <response code="200">Статус оновлено</response>
    /// <response code="400">Некоректне значення в тілі запиту</response>
    /// <response code="404">Замовлення з таким <paramref name="id"/> не знайдено</response>
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