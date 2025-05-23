using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Grade;

public class Add : PageModel
{
    [BindProperty]
    public GradeDto  grade { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IGradeService _service;

    public Add(IGradeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        grade = new GradeDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            grade = response.Data;
        }

        if (grade == null)
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

        Response<GradeDto> response;
        if (grade.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(grade);
        }
        else
        {
            response = await _service.SaveAsync(grade);
        }
        
        grade = response.Data;
        return RedirectToPage("./List");
    }
}