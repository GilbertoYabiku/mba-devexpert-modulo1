namespace MBADevExpertModulo1.Core.Models;
public class JWTSettings
{
    public string? Secret { get; set; }
    public int HoursUntilExpiration { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}
