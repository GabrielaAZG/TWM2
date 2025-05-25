using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject_Grade;

public class List : PageModel
{
    private readonly ISubject_GradeService _service;
    public List<Subject_GradeDto> Subjects_Grades { get; set; }
    

    public List(ISubject_GradeService service)
    {
        Subjects_Grades = new List<Subject_GradeDto>();
        _service = service;
    }
    
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Subjects_Grades = response.Data;
        return Page();
    }
}