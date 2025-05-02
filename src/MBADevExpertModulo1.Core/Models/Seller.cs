namespace MBADevExpertModulo1.Core.Models;

public class Seller : BaseModel
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public ICollection<Product>? Products { get; set; }
}

