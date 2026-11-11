using System.Runtime.InteropServices.JavaScript;

namespace WebApiP41.Models;

public class Order
{
    public int Id{ get; set;}
    public int OrderNumber{ get; set;}
    public DateTime Date{ get; set;}
    public int Quantity{ get; set;}
    public decimal Cost{ get; set;}
    public bool IsComplete{ get; set;}
    public string Comment{ get; set;}
}