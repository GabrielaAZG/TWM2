using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Pages.Teacher;


public class List : PageModel
{
    private readonly ITeacherService _service;
    
    public List<TeacherDto> Teachers { get; set; }
    
    public TeacherDto Teacher { get; set; }

    public List(ITeacherService service)
    {
        Teachers = new List<TeacherDto>();
        _service = service;
    }
    public async Task<ActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        
        if (response == null || response.Data == null)
        {
            // Puedes registrar un error, mostrar un mensaje o manejarlo como desees
            ModelState.AddModelError(string.Empty, "No se pudo obtener la lista de maestros.");
            Teachers = new List<TeacherDto>(); // Para evitar que se quede null
        }
        else
        {
            Teachers = response.Data;
        }
        return Page();

    }
}