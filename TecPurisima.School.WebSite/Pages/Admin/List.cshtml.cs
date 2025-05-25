using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Admin;

public class List : PageModel
{
    private readonly IAdminService  _service;
    public List<AdminDto> Admins { get; set; }
    
    public AdminDto Admin { get; set; }

    public List(IAdminService service)
    {
        Admins = new List<AdminDto>();
        _service = service;
    }
    
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Admins = response.Data;
        return Page();
    }
}