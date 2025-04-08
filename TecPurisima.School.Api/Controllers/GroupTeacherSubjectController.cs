using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController] //Esto hace que la clase sea un endpoint
[Route("api/[controller]")]

public class GroupTeacherSubjectController:ControllerBase
{
    private readonly IGroupTeacherSubject _groupTeacherSubject;
    private readonly IGroupRepository _groupRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;
    public GroupTeacherSubjectController(IGroupTeacherSubject groupTeacherSubject, IGroupRepository groupRepository, ITeacherRepository teacherRepository, ISubjectRepository subjectRepository) //Constructor del controlador
    {
        _groupTeacherSubject = groupTeacherSubject;
        _groupRepository = groupRepository;
        _teacherRepository = teacherRepository;
        _subjectRepository = subjectRepository;

    }
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Group_Teacher_Subject>>>> GetAll() //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<List<GroupTeacherSubjectDto>>();
        var groupTeacherSubjects = await _groupTeacherSubject.GetAllAsync();
        
        response.Data = groupTeacherSubjects.Select(c => new GroupTeacherSubjectDto(c)).ToList();
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<GroupTeacherSubjectDto>>> 
        Post([FromBody] GroupTeacherSubjectDto groupTeacherSubjectDto) //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<GroupTeacherSubjectDto>();
        
        //Agregado
        var groupExists = await _groupRepository.ExistsAsync(groupTeacherSubjectDto.GroupId);
        var teacherExists = await _teacherRepository.ExistsAsync(groupTeacherSubjectDto.TeacherId);
        var subjectExists = await _subjectRepository.ExistsAsync(groupTeacherSubjectDto.SubjectId);
        
        if (!groupExists || !teacherExists || !subjectExists)
        {
            var errors = new List<string>();
            if (!groupExists) errors.Add($"GroupId {groupTeacherSubjectDto.GroupId} don't exist");
            if (!teacherExists) errors.Add($"TeacherId {groupTeacherSubjectDto.TeacherId} don't exist");
            if (!subjectExists) errors.Add($"SubjectId {groupTeacherSubjectDto.SubjectId} don't exist");

            response.Message = "Error";
            response.Errors = errors;
            return BadRequest(response);
        }
        
        var groupts = new Group_Teacher_Subject()
        {
            GroupId = groupTeacherSubjectDto.GroupId,
            TeacherId = groupTeacherSubjectDto.TeacherId,
            SubjectId = groupTeacherSubjectDto.SubjectId,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now,
        };
        groupts = await _groupTeacherSubject.SaveAsync(groupts);
        groupts.Id = groupts.Id;
        response.Data =groupTeacherSubjectDto;
        return Created($"/api/[controller]/{groupTeacherSubjectDto.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<GroupTeacherSubjectDto>>> GetById(int id)
    {
        var response = new Response<GroupTeacherSubjectDto>();
        var groupts = await _groupTeacherSubject.GetById(id);
        
        if (groupts == null)
        {
            response.Errors.Add(("Student Not Found"));
            return NotFound(response);
        }
        var grouptsDto = new GroupTeacherSubjectDto(groupts);
        response.Data = grouptsDto;
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        
        var boolResult = await _groupTeacherSubject.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = boolResult;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<GroupTeacherSubjectDto>>> 
        Update([FromBody] GroupTeacherSubjectDto groupTeacherSubjectDto)
    {
        var response = new Response<GroupTeacherSubjectDto>();
        var groupts = await _groupTeacherSubject.GetById(groupTeacherSubjectDto.Id);
        if (groupts == null)
        {
            response.Errors.Add(("Student Not Found"));
            return NotFound(response);
        }
        groupts.GroupId = groupTeacherSubjectDto.GroupId;
        groupts.TeacherId = groupTeacherSubjectDto.TeacherId;
        groupts.SubjectId = groupTeacherSubjectDto.SubjectId;
        groupts.UpdatedBy = "";
        groupts.UpdatedDate = DateTime.Now;
        await _groupTeacherSubject.UpdateAsync(groupts);
        response.Data = groupTeacherSubjectDto;
        return Ok(response);
    }
}