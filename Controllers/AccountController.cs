using System.ComponentModel.DataAnnotations;
using MedicalDeviceSafetyPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDeviceSafetyPlatform.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;
    public AccountController(UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn)
    { _users = users; _signIn = signIn; }

    [HttpGet] public IActionResult Register() => View(new RegisterInput());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterInput input)
    {
        if (!ModelState.IsValid) return View(input);
        var user = new ApplicationUser { UserName = input.Email, Email = input.Email };
        var result = await _users.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(input);
        }
        var roleResult = await _users.AddToRoleAsync(user, "Employee");
        if (!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors) ModelState.AddModelError("", error.Description);
            return View(input);
        }
        await _signIn.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet] public IActionResult Login(string? returnUrl = null) => View(new LoginInput { ReturnUrl = returnUrl });
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginInput input)
    {
        if (!ModelState.IsValid) return View(input);
        var result = await _signIn.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: true);
        if (result.Succeeded) return LocalRedirect(Url.IsLocalUrl(input.ReturnUrl) ? input.ReturnUrl! : "/");
        ModelState.AddModelError("", "Invalid login credentials.");
        return View(input);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
public class RegisterInput
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
}
public class LoginInput
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    public string? ReturnUrl { get; set; }
}
