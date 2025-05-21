using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    public TeachersController(ITeacherService teacherService) //Constructor del controlador
    {
        
        _teacherService = teacherService;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Teacher>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<TeacherDto>>
        {
            Data = await _teacherService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<TeacherDto>>> Post([FromBody] TeacherDto teacherDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<TeacherDto>
        {
            Data = await _teacherService.SaveAsync(teacherDto)
        };

        return Created($"/api/[controller]/{response.Data.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<TeacherDto>>> GetById(int id)
    {
        var response = new Response<TeacherDto>();
        
        if (!await _teacherService.TeacherExist(id))
        {
            response.Errors.Add(("Teacher Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _teacherService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _teacherService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<TeacherDto>>> Update([FromBody] TeacherDto teacherDto)
    {
        var response = new Response<TeacherDto>();
        
        if (!await _teacherService.TeacherExist(teacherDto.Id))
        {
            response.Errors.Add(("Teacher Not Found"));
            return NotFound(response);
        }
        
        response.Data = await _teacherService.UpdateAsync(teacherDto);
        return Ok(response);
    }
}