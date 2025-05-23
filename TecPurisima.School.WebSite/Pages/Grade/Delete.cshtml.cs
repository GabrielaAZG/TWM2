using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Grade;

public class Delete : PageModel
{
    [BindProperty]
    public GradeDto grade { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IGradeService _service;

    public Delete(IGradeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        grade = new GradeDto();
        var response = await _service.GetByIdAsync(id);
        grade = response.Data;

        if (grade == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(grade.Id);
        return RedirectToPage("./List");
    }
}