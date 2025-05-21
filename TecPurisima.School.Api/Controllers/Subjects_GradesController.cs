using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class Subjects_GradesController : ControllerBase
{
    private readonly ISubject_GradeService _subjectgradeService;
    public Subjects_GradesController(ISubject_GradeService subjectgradeService) //Constructor del controlador
    {
        
        _subjectgradeService = subjectgradeService;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Subject_Grade>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<Subject_GradeDto>>
        {
            Data = await _subjectgradeService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<Subject_GradeDto>>> Post([FromBody] Subject_GradeDto subjectgradeDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<Subject_GradeDto>();
        try
        {
            response.Data = await _subjectgradeService.SaveAsync(subjectgradeDto);
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
    public async Task<ActionResult<Response<Subject_GradeDto>>> GetById(int id)
    {
        var response = new Response<Subject_GradeDto>();
        
        if (!await _subjectgradeService.Subject_GradeExist(id))
        {
            response.Errors.Add(("Subject and Grade Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _subjectgradeService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _subjectgradeService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<Subject_GradeDto>>> Update([FromBody] Subject_GradeDto subjectgradeDto)
    {
        var response = new Response<Subject_GradeDto>();
        
        if (!await _subjectgradeService.Subject_GradeExist(subjectgradeDto.Id))
        {
            response.Errors.Add(("Subject and Grade Not Found"));
            return NotFound(response);
        }

        try
        {
            response.Data = await _subjectgradeService.UpdateAsync(subjectgradeDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Errors.Add(ex.Message);
            return BadRequest(response);
        }
        
    }
}