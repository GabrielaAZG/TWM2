using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Grade;

public class List : PageModel
{
    private readonly IGradeService _service;
    
    public List<GradeDto> Grades { get; set; }
    
    public GradeDto Grade { get; set; }

    public List(IGradeService service)
    {
        Grades = new List<GradeDto>();
        _service = service;
    }
    
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Grades = response.Data;
        return Page();
    }
}