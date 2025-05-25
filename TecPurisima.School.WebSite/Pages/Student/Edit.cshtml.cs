using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Student;

public class Edit : PageModel
{
    [BindProperty]
    public StudentDto  student { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    private readonly IStudentService _service;

    public Edit(IStudentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        student = new StudentDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            student= response.Data;
        }

        if (student == null)
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

        Response<StudentDto> response;
        if (student.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(student);
        }
        else
        {
            response = await _service.SaveAsync(student);
        }
        
        student = response.Data;
        return RedirectToPage("./List");
    }
}