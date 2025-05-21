using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    public GroupsController(IGroupService groupService) //Constructor del controlador
    {
        _groupService = groupService;
    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<SchoolGroup>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<SchoolGroupDto>>
        {
            Data = await _groupService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<SchoolGroupDto>>> Post([FromBody] SchoolGroupDto groupDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<SchoolGroupDto>();
        try
        {

            response.Data = await _groupService.SaveAsync(groupDto);
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
    public async Task<ActionResult<Response<SchoolGroupDto>>> GetById(int id)
    {
        var response = new Response<SchoolGroupDto>();
        
        if (!await _groupService.SchoolGroupExist(id))
        {
            response.Errors.Add(("School Group Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _groupService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _groupService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<SchoolGroupDto>>> Update([FromBody] SchoolGroupDto groupDto)
    {
        var response = new Response<SchoolGroupDto>();
        
        if (!await _groupService.SchoolGroupExist(groupDto.Id))
        {
            response.Errors.Add(("School Group Not Found"));
            return NotFound(response);
        }

        try
        {
            response.Data = await _groupService.UpdateAsync(groupDto);
            return Ok(response);
        }catch(Exception ex)
        {
            response.Errors.Add(ex.Message);
            return BadRequest(response);
        }
        
        
    }
}