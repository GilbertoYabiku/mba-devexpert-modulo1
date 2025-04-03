using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using MBADevExpertModulo1.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MBADevExpertModulo1.WebAPI.Controllers;
[ApiController]
[Route("api/account")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager; 
    private readonly JWTSettings _jwtSettings;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JWTSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUser registerUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState));

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok(await GenerateJWT(user.Email));
        }

        return Problem("Failed to register new user");
    }

    [HttpPost("login")]
    public async Task<ActionResult<ICollection<Category>>> Login(LoginUser loginUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState));

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
        {
            return Ok(await GenerateJWT(loginUser.Email));
        }

        return Problem("Invalid user or password");
    }

    private string GenerateJWT()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.HoursUntilExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return encodedToken;
    }
}
