namespace WebApiP41.Models.DTO;

public class OrderViewModel
{
    public int Id{ get; set;}
    public string OrderNumberFormatted{ get; set;}
    public DateTime Date{ get; set;}
    public int Quantity{ get; set;}
    public decimal Cost{ get; set;}
    public decimal TotalPrice { get; set; }
    public string CompletedStatus{ get; set;}
    public string Comment{ get; set;}
}