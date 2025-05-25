using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Group;

public class Delete : PageModel
{
    [BindProperty]
    public SchoolGroupDto Group { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IGroupService _service;

    public Delete(IGroupService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Group = new SchoolGroupDto();
        var response = await _service.GetByIdAsync(id);
        Group = response.Data;

        if (Group == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(Group.Id);
        return RedirectToPage("./List");
    }
}