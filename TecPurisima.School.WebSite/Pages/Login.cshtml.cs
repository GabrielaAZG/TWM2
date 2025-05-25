using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TecPurisima.School.Api.Services;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.WebSite.Pages;

public class LoginModel : PageModel
{
    private readonly AdminService _adminService;

    public LoginModel(AdminService adminService)
    {
        _adminService = adminService;
    }

    [BindProperty] public string User { get; set; }
    [BindProperty] public string Password { get; set; }
    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var admin = await _adminService.LoginAsync(User, Password);

        if (admin == null)
        {
            ErrorMessage = "Usuario o contraseña incorrectos.";
            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, admin.FullName),
            new Claim("AdminId", admin.Id.ToString()),
            new Claim(ClaimTypes.Role, admin.Role)
            
        };

        var identity = new ClaimsIdentity(claims, "AdminCookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("AdminCookie", principal);
        Console.WriteLine("Cookie para "+admin.FullName);

        return RedirectToPage("/Index");
    }
}