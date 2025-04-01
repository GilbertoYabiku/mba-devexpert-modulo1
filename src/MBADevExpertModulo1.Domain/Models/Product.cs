namespace MBADevExpertModulo1.Domain.Models;

public class Product : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Image {  get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; }
}
