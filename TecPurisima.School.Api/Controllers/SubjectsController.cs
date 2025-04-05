using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;


namespace TecPurisima.School.Api.Controllers;


[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]

public class SubjectsController : ControllerBase
{
    private readonly ISubjectRepository _subjectRepository;
    public SubjectsController(ISubjectRepository subjectRepository) //Constructor del controlador
    {
        _subjectRepository = subjectRepository;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Subject>>>> GetAll() //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<List<SubjectDto>>();
        var categories = await _subjectRepository.GetAllAsync();
        
        response.Data = categories.Select(c => new SubjectDto(c)).ToList();
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<SubjectDto>>> Post([FromBody] SubjectDto subjectDto) //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<SubjectDto>();
        var subject = new Subject()
        {
            SubjectName = subjectDto.SubjectName,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        subject = await _subjectRepository.SaveAsync(subject);
        subject.Id = subject.Id;
        response.Data = subjectDto;
        return Created($"/api/[controller]/{subjectDto.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<SubjectDto>>> GetById(int id)
    {
        var response = new Response<SubjectDto>();
        var subject = await _subjectRepository.GetById(id);
        
        if (subject == null)
        {
            response.Errors.Add(("Subject Not Found"));
            return NotFound(response);
        }
        var subjectDto = new SubjectDto(subject);
        response.Data = subjectDto;
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        
        var boolResult = await _subjectRepository.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = boolResult;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<SubjectDto>>> Update([FromBody] SubjectDto subjectDto)
    {
        var response = new Response<SubjectDto>();
        var subject = await _subjectRepository.GetById(subjectDto.Id);
        if (subject == null)
        {
            response.Errors.Add(("Subject Not Found"));
            return NotFound(response);
        }
        subject.SubjectName = subjectDto.SubjectName;
        subject.UpdatedBy = "";
        subject.UpdatedDate = DateTime.Now;
        await _subjectRepository.UpdateAsync(subject);
        response.Data = subjectDto;
        return Ok(response);
    }
}