using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject;

public class Add : PageModel
{
    
    [BindProperty]
    public SubjectDto Subjects { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ISubjectService _service;

    public Add(ISubjectService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        Subjects = new SubjectDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Subjects = response.Data;
        }

        if (Subjects == null)
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
        if (Subjects.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Subjects);
        }
        else
        {
            response = await _service.SaveAsync(Subjects);
        }
        
        Subjects = response.Data;
        return RedirectToPage("./List");
    }
}