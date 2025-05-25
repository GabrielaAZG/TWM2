using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject_Grade;

public class Delete : PageModel
{
    [BindProperty]
    public Subject_GradeDto Subject_Grade { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ISubject_GradeService _service;

    public Delete(ISubject_GradeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Subject_Grade = new Subject_GradeDto();
        var response = await _service.GetByIdAsync(id);
        Subject_Grade = response.Data;

        if (Subject_Grade == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(Subject_Grade.Id);
        return RedirectToPage("./List");
    }
}