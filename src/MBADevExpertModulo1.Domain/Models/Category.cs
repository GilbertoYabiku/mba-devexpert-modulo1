using System.ComponentModel.DataAnnotations;

namespace MBADevExpertModulo1.Domain.Models; 

public class Category : BaseModel
{
    [Required(ErrorMessage = "{0} is a required field")]
    public string Name { get; set; }
    [Required(ErrorMessage = "{0} is a required field")]
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; }
}
