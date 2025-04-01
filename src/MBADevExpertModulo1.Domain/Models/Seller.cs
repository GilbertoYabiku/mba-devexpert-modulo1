namespace MBADevExpertModulo1.Domain.Models;

public class Seller : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Product> Products { get; set; }
}

