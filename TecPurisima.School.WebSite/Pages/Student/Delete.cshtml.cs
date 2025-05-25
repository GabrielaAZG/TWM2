using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Student;

public class Delete : PageModel
{
    [BindProperty]
    public StudentDto Student { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IStudentService _service;

    public Delete(IStudentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Student = new StudentDto();
        var response = await _service.GetByIdAsync(id);
        Student = response.Data;

        if (Student == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(Student.Id);
        return RedirectToPage("./List");
    }
}