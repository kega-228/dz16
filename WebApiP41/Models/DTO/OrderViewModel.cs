namespace WebApiP41.Models.DTO;

/// <summary>
/// Модель відображення замовлення для клієнта.
/// </summary>
public class OrderViewModel
{
    /// <summary>
    /// Унікальний ідентифікатор замовлення.
    /// </summary>
    public int Id{ get; set;}

    /// <summary>
    /// Форматований номер замовлення.
    /// </summary>
    public string OrderNumberFormatted{ get; set;}

    /// <summary>
    /// Дата створення замовлення.
    /// </summary>
    public DateTime Date{ get; set;}

    /// <summary>
    /// Кількість товару.
    /// </summary>
    public int Quantity{ get; set;}

    /// <summary>
    /// Вартість одиниці товару.
    /// </summary>
    public decimal Cost{ get; set;}

    /// <summary>
    /// Загальна вартість замовлення.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Статус виконання замовлення (текстовий опис).
    /// </summary>
    public string CompletedStatus{ get; set;}

    /// <summary>
    /// Коментар до замовлення.
    /// </summary>
    public string Comment{ get; set;}
}