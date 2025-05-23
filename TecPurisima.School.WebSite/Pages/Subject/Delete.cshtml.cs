using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject;

public class Delete : PageModel
{
    [BindProperty]
    public SubjectDto Subject { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ISubjectService _service;

    public Delete(ISubjectService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        Subject = new SubjectDto();
        var response = await _service.GetByIdAsync(id);
        Subject = response.Data;

        if (Subject == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await _service.DeleteAsync(Subject.Id);
        return RedirectToPage("./List");
    }
}