using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace MBADevExpertModulo1.Core.Models;

public class Product : BaseModel
{
    [Required(ErrorMessage = "{0} is a required field")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public string? Image {  get; set; }
    public IFormFile? ImageFormFile { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    [Range(0, double.MaxValue, ErrorMessage = "{0} cannot be a negative number")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    [Range(0, int.MaxValue, ErrorMessage = "{0} cannot be a negative number")]
    public int Stock { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public Guid SellerId { get; set; }
    public Seller? Seller { get; set; }
}
