using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Teacher;

public class Add : PageModel
{
    [BindProperty]
    public TeacherDto Teacher { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly ITeacherService _service;

    public Add(ITeacherService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        Teacher = new TeacherDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Teacher = response.Data;
        }

        if (Teacher == null)
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

        Response<TeacherDto> response;
        if (Teacher.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Teacher);
        }
        else
        {
            response = await _service.SaveAsync(Teacher);
        }
        
        Teacher = response.Data;
        return RedirectToPage("./List");
    }
}