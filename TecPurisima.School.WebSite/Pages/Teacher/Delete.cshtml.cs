using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Teacher;

public class Delete : PageModel
{
    [BindProperty]
    public TeacherDto Teacher { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ITeacherService _service;

    public Delete(ITeacherService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Teacher = new TeacherDto();
        var response = await _service.GetByIdAsync(id);
        Teacher = response.Data;

        if (Teacher == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(Teacher.Id);
        return RedirectToPage("./List");
    }
   
}