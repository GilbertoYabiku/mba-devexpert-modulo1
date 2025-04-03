using System.ComponentModel.DataAnnotations;

namespace MBADevExpertModulo1.Domain.Models;

public class Product : BaseModel
{
    [Required(ErrorMessage = "{0} is a required field")]
    public string Name { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public string Description { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public byte[] Image {  get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    [Range(0, double.MaxValue, ErrorMessage = "{0} cannot be a negative number")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    [Range(0, int.MaxValue, ErrorMessage = "{0} cannot be a negative number")]
    public int Stock { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public int SellerId { get; set; }
    public Seller Seller { get; set; }
}
