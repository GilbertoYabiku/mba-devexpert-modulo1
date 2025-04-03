using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBADevExpertModulo1.Domain.Models;
public class JWTSettings
{
    public string? Secret { get; set; }
    public int HoursUntilExpiration { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}
