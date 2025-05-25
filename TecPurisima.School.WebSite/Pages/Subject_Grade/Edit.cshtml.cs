using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Subject_Grade;

public class Edit : PageModel
{
    [BindProperty]
    public Subject_GradeDto Subject_Grade { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ISubject_GradeService _service;

    public Edit(ISubject_GradeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        Subject_Grade = new Subject_GradeDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Subject_Grade = response.Data;
        }

        if (Subject_Grade == null)
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

        Response<Subject_GradeDto> response;
        if (Subject_Grade.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Subject_Grade);
        }
        else
        {
            response = await _service.SaveAsync(Subject_Grade);
        }
        
        Subject_Grade = response.Data;
        return RedirectToPage("./List");
    }
}