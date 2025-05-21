using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;
    public GradesController(IGradeService gradeService) //Constructor del controlador
    {
        _gradeService = gradeService;
    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Grade>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<GradeDto>>
        {
            Data = await _gradeService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<GradeDto>>> Post([FromBody] GradeDto gradeDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<GradeDto>
        {
            Data = await _gradeService.SaveAsync(gradeDto)
        };

        return Created($"/api/[controller]/{response.Data.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<GradeDto>>> GetById(int id)
    {
        var response = new Response<GradeDto>();
        
        if (!await _gradeService.GradeExist(id))
        {
            response.Errors.Add(("School Grade Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _gradeService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _gradeService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<GradeDto>>> Update([FromBody] GradeDto gradeDto)
    {
        var response = new Response<GradeDto>();
        
        if (!await _gradeService.GradeExist(gradeDto.Id))
        {
            response.Errors.Add(("School Grade Not Found"));
            return NotFound(response);
        }
        
        response.Data = await _gradeService.UpdateAsync(gradeDto);
        return Ok(response);
    }
}