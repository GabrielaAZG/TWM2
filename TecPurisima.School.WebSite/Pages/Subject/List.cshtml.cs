using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject;

public class List : PageModel
{
    private readonly ISubjectService _service;
    public List<SubjectDto> Subjects { get; set; }
    
    public SubjectDto Subject { get; set; }

    public List(ISubjectService service)
    {
        Subjects = new List<SubjectDto>();
        _service = service;
    }
    
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Subjects = response.Data;
        return Page();
    }
}