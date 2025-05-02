using System.Security.Claims;
using MBADevExpertModulo1.Core.Interfaces;
using MBADevExpertModulo1.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.App.Controllers;

[Route("users")]
public class UsersController(ISellerRepository sellerRepository,
                         SignInManager<IdentityUser> signInManager,
                         UserManager<IdentityUser> userManager) : Controller
{
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUser model)
    {
        if (ModelState.IsValid)
        {
            await signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Index", "Home");
    }

    [Route("signup")]
    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(LoginUser model)
    {
        if (ModelState.IsValid)
        {

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Seller seller = new()
                {
                    Id = Guid.Parse(userId),
                    Name = model.Email,
                    Email = model.Email,
                    Deleted = false
                };

                await sellerRepository.AddSellerAsync(seller);
                return RedirectToAction("Index", "Home");
            }
        }
        return RedirectToAction("Index", "Home");
    }

    [Route("signout")]
    public async Task<IActionResult> Signout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
