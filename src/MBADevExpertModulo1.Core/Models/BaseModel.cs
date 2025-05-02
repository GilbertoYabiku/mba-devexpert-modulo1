namespace MBADevExpertModulo1.Core.Models;

public class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Deleted { get; set; }
}

