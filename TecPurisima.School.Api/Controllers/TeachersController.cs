using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]

public class TeachersController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;
    public TeachersController(ITeacherRepository teacherRepository) //Constructor del controlador
    {
        _teacherRepository = teacherRepository;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Teacher>>>> GetAll() //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<List<TeacherDto>>();
        var teachers = await _teacherRepository.GetAllAsync();
        
        response.Data = teachers.Select(c => new TeacherDto(c)).ToList();
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<TeacherDto>>> Post([FromBody] TeacherDto teacherDto) //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<TeacherDto>();
        var teacher = new Teacher()
        {
            FullName = teacherDto.FullName,
            Email = teacherDto.Email,
            Gender = teacherDto.Gender,
            CURP = teacherDto.CURP,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        teacher = await _teacherRepository.SaveAsync(teacher);
        teacher.Id = teacher.Id;
        response.Data = teacherDto;
        return Created($"/api/[controller]/{teacherDto.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<TeacherDto>>> GetById(int id)
    {
        var response = new Response<TeacherDto>();
        var teacher = await _teacherRepository.GetById(id);
        
        if (teacher == null)
        {
            response.Errors.Add(("Teacher Not Found"));
            return NotFound(response);
        }
        var teacherDto = new TeacherDto(teacher);
        response.Data = teacherDto;
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        
        var boolResult = await _teacherRepository.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = boolResult;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<TeacherDto>>> Update([FromBody] TeacherDto teacherDto)
    {
        var response = new Response<TeacherDto>();
        var teacher = await _teacherRepository.GetById(teacherDto.Id);
        if (teacher == null)
        {
            response.Errors.Add(("Teacher Not Found"));
            return NotFound(response);
        }
        teacher.FullName = teacherDto.FullName;
        teacher.Email = teacherDto.Email;
        teacher.Gender = teacherDto.Gender;
        teacher.CURP = teacherDto.CURP;
        teacher.UpdatedBy = "";
        teacher.UpdatedDate = DateTime.Now;
        await _teacherRepository.UpdateAsync(teacher);
        response.Data = teacherDto;
        return Ok(response);
    }
}