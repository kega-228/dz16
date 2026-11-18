using System.ComponentModel.DataAnnotations;

namespace WebApiP41.Models.DTO;

public class CategoryFilter
{
    public int? Id{ get; set; }
    public string? Name{get;set;}
}