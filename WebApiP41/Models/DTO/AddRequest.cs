using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiP41.Models.DTO;

public class AddRequest
{
    [Required(ErrorMessage = "Please enter the order number")]
    [Range(1, int.MaxValue, ErrorMessage = "Order number must be a positive integer")]
    [Description("The quantity of the product to be ordered")]
    public int Quantity{ get; set;}
    [Required(ErrorMessage = "Product cost is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Product cost must be a positive number")]
    [Description("The cost of the product to be ordered")]
    public decimal Cost{ get; set;}
    [Description("Any comments about the order")]
    [MaxLength(500, ErrorMessage = "Comment must be at most 500 characters long")]
    public string Comment{ get; set;}
}