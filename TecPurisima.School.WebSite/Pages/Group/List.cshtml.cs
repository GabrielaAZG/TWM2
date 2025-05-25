using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Group;

public class List : PageModel
{
    private readonly IGroupService _service;
    public List<SchoolGroupDto> Groups { get; set; }
    
    public SchoolGroupDto Group { get; set; }

    public List(IGroupService service)
    {
        Groups = new List<SchoolGroupDto>();
        _service = service;
    }
    
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Groups = response.Data;
        return Page();
    }
}