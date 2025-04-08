using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]

public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGroupRepository _groupRepository;
    public StudentsController(IStudentRepository studentRepository, IGroupRepository groupRepository) //Constructor del controlador
    {
        _studentRepository = studentRepository;
        _groupRepository = groupRepository;

    }
    
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Student>>>> GetAll() //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<List<StudentDto>>();
        var students = await _studentRepository.GetAllAsync();
        
        response.Data = students.Select(c => new StudentDto(c)).ToList();
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<StudentDto>>> Post([FromBody] StudentDto studentDto) //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<StudentDto>();
        
        var groupExists = await _groupRepository.ExistsAsync(studentDto.GroupId);
        
        if (!groupExists)
        {
            var errors = new List<string>();
            if (!groupExists) errors.Add($"GroupId {studentDto.GroupId} don't exist");

            response.Message = "Error";
            response.Errors = errors;
            return BadRequest(response);
        }
        
        
        var student = new Student()
        {
            FullName = studentDto.FullName,
            Gender = studentDto.Gender,
            CURP = studentDto.CURP,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now,
            GroupId = studentDto.GroupId
        };
        student = await _studentRepository.SaveAsync(student);
        student.Id = student.Id;
        response.Data = studentDto;
        return Created($"/api/[controller]/{studentDto.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<StudentDto>>> GetById(int id)
    {
        var response = new Response<StudentDto>();
        var student = await _studentRepository.GetById(id);
        
        if (student == null)
        {
            response.Errors.Add(("Student Not Found"));
            return NotFound(response);
        }
        var studentDto = new StudentDto(student);
        response.Data = studentDto;
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        
        var boolResult = await _studentRepository.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = boolResult;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<StudentDto>>> Update([FromBody] StudentDto studentDto)
    {
        var response = new Response<StudentDto>();
        var student = await _studentRepository.GetById(studentDto.Id);
        if (student == null)
        {
            response.Errors.Add(("Student Not Found"));
            return NotFound(response);
        }
        student.FullName = studentDto.FullName;
        student.Gender = studentDto.Gender;
        student.CURP = studentDto.CURP;
        student.UpdatedBy = "";
        student.UpdatedDate = DateTime.Now;
        student.GroupId = studentDto.GroupId;
        await _studentRepository.UpdateAsync(student);
        response.Data = studentDto;
        return Ok(response);
    }
    
}