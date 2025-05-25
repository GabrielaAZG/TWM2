using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Group;

public class Edit : PageModel
{
    [BindProperty]
    public SchoolGroupDto Groups { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IGroupService _service;

    public Edit(IGroupService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        Groups = new SchoolGroupDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Groups = response.Data;
        }

        if (Groups == null)
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

        Response<SchoolGroupDto> response;
        if (Groups.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Groups);
        }
        else
        {
            response = await _service.SaveAsync(Groups);
        }
        
        Groups = response.Data;
        return RedirectToPage("./List");
    }
}