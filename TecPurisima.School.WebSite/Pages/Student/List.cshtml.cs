using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Student;

public class List : PageModel
{
    private readonly IStudentService _service;
    public List<StudentDto> Students { get; set; }
    
    public StudentDto Student { get; set; }

    public List(IStudentService service)
    {
        Students = new List<StudentDto>();
        _service = service;
    }
    
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Students = response.Data;
        return Page();
    }
}