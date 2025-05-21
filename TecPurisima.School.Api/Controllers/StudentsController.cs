using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentsController(IStudentService studentService) //Constructor del controlador
    {
        
        _studentService = studentService;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Student>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<StudentDto>>
        {
            Data = await _studentService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<StudentDto>>> Post([FromBody] StudentDto studentDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<StudentDto>();
        try
        {
            response.Data = await _studentService.SaveAsync(studentDto);
            return Created($"/api/[controller]/{response.Data.Id}", response);
        }
        catch (Exception ex)
        {
            response.Errors.Add(ex.Message);
            return BadRequest(response);
        }
        
            
        

        

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<StudentDto>>> GetById(int id)
    {
        var response = new Response<StudentDto>();
        
        if (!await _studentService.StudentExist(id))
        {
            response.Errors.Add(("Student Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _studentService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _studentService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<StudentDto>>> Update([FromBody] StudentDto studentDto)
    {
        var response = new Response<StudentDto>();
        
        if (!await _studentService.StudentExist(studentDto.Id))
        {
            response.Errors.Add(("Student Not Found"));
            return NotFound(response);
        }

        try
        {
            response.Data = await _studentService.UpdateAsync(studentDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Errors.Add(ex.Message);
            return BadRequest(response);
        }
        
    }
}