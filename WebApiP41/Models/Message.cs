using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApiP41.Models;

public class Message
{
    [ValidateNever]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter your email address")]
    [MinLength(5, ErrorMessage = "Email address must be at least 5 characters long")]
    [MaxLength(100, ErrorMessage = "Email address must be at most 100 characters long")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string FromAddress { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please enter a message")]
    [MinLength(10, ErrorMessage = "Message must be at least 10 characters long")]
    [MaxLength(500, ErrorMessage = "Message must be at most 500 characters long")]
    public string Text { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please enter a date and time")]
    public DateTime DateTime { get; set; }
}
