using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject;

public class Edit : PageModel
{
    [BindProperty]
    public SubjectDto Subject { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ISubjectService _service;

    public Edit(ISubjectService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        Subject = new SubjectDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Subject = response.Data;
        }

        if (Subject == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<SubjectDto> response;
        if (Subject.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Subject);
        }
        else
        {
            response = await _service.SaveAsync(Subject);
        }
        
        Subject = response.Data;
        return RedirectToPage("./List");
    }
}