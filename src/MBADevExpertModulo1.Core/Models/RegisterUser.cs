using System.ComponentModel.DataAnnotations;

namespace MBADevExpertModulo1.Core.Models;
public class RegisterUser
{
    [Required(ErrorMessage = "{0} is a required field")]
    [EmailAddress(ErrorMessage = "{0} invalid format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} is a required field")]
    [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 6)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string? ConfirmPassword { get; set; }
}
