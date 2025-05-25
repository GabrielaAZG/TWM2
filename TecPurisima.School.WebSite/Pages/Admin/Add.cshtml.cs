using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Admin;

public class Add : PageModel
{
    [BindProperty]
    public AdminDto  admin { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IAdminService _service;

    public Add(AdminService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        admin = new AdminDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            admin = response.Data;
        }

        if (admin == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<AdminDto> response;
        if (admin.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(admin);
        }
        else
        {
            response = await _service.SaveAsync(admin);
        }
        
        admin = response.Data;
        return RedirectToPage("./List");
    }
}