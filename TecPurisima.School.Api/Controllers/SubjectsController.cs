using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    public SubjectsController(ISubjectService subjectService) //Constructor del controlador
    {
        
        _subjectService = subjectService;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<SubjectDto>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<SubjectDto>>
        {
            Data = await _subjectService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<SubjectDto>>> Post([FromBody] SubjectDto subjectDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<SubjectDto>
        {
            Data = await _subjectService.SaveAsync(subjectDto)
        };

        return Created($"/api/[controller]/{response.Data.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<SubjectDto>>> GetById(int id)
    {
        var response = new Response<SubjectDto>();
        
        if (!await _subjectService.SubjectExist(id))
        {
            response.Errors.Add(("Subject Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _subjectService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _subjectService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<SubjectDto>>> Update([FromBody] SubjectDto subjectDto)
    {
        var response = new Response<SubjectDto>();
        
        if (!await _subjectService.SubjectExist(subjectDto.Id))
        {
            response.Errors.Add(("Subject Not Found"));
            return NotFound(response);
        }
        
        response.Data = await _subjectService.UpdateAsync(subjectDto);
        return Ok(response);
    }
}