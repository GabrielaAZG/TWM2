using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Admin;

public class Delete : PageModel
{
    [BindProperty]
    public AdminDto admin { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IAdminService _service;

    public Delete(IAdminService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        admin = new AdminDto();
        var response = await _service.GetByIdAsync(id);
        admin = response.Data;

        if (admin == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(admin.Id);
        return RedirectToPage("./List");
    }
}