using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]

public class GroupsController:ControllerBase
{
    private readonly IGroupRepository _groupRepository;
    public GroupsController(IGroupRepository groupRepository) //Constructor del controlador
    {
        _groupRepository = groupRepository;

    }
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<SchoolGroup>>>> GetAll() //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<List<SchoolGroupDto>>();
        var groups = await _groupRepository.GetAllAsync();
        
        response.Data = groups.Select(c => new SchoolGroupDto(c)).ToList();
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<SchoolGroupDto>>> Post([FromBody] SchoolGroupDto groupDto) //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<SchoolGroupDto>();
        var teacher = new SchoolGroup()
        {
            GroupName = groupDto.GroupName,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        teacher = await _groupRepository.SaveAsync(teacher);
        teacher.Id = teacher.Id;
        response.Data = groupDto;
        return Created($"/api/[controller]/{groupDto.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<SchoolGroup>>> GetById(int id)
    {
        var response = new Response<SchoolGroupDto>();
        var group = await _groupRepository.GetById(id);
        
        if (group == null)
        {
            response.Errors.Add(("Teacher Not Found"));
            return NotFound(response);
        }
        var groupDto = new SchoolGroupDto(group);
        response.Data = groupDto;
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        
        var boolResult = await _groupRepository.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = boolResult;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<SchoolGroupDto>>> Update([FromBody] SchoolGroupDto groupDto)
    {
        var response = new Response<SchoolGroupDto>();
        var group = await _groupRepository.GetById(groupDto.Id);
        if (group == null)
        {
            response.Errors.Add(("Teacher Not Found"));
            return NotFound(response);
        }
        group.GroupName = groupDto.GroupName;
        group.UpdatedBy = "";
        group.UpdatedDate = DateTime.Now;
        await _groupRepository.UpdateAsync(group);
        response.Data = groupDto;
        return Ok(response);
    }
}