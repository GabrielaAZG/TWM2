using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository) //Constructor del controlador
    {
        _userRepository = userRepository;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<User>>>> GetAll() //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<List<UserDto>>();
        var users = await _userRepository.GetAllAsync();
        
        response.Data = users.Select(c => new UserDto(c)).ToList();
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<UserDto>>> Post([FromBody] UserDto userDto) //Metodo GetAll devuelve todas las categorias y devuelve un objeto Response que contiene la lista ProductCategory
    {
        var response = new Response<UserDto>();
        var user = new User()
        {
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        user = await _userRepository.SaveAsync(user);
        user.Id = user.Id;
        response.Data = userDto;
        return Created($"/api/[controller]/{userDto.Id}", response);

    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<UserDto>>> GetById(int id)
    {
        var response = new Response<UserDto>();
        var user = await _userRepository.GetById(id);
        
        if (user == null)
        {
            response.Errors.Add(("User Not Found"));
            return NotFound(response);
        }
        var userDto = new UserDto(user);
        response.Data = userDto;
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        
        var boolResult = await _userRepository.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = boolResult;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<UserDto>>> Update([FromBody] UserDto userDto)
    {
        var response = new Response<UserDto>();
        var user = await _userRepository.GetById(userDto.Id);
        if (user == null)
        {
            response.Errors.Add(("User Not Found"));
            return NotFound(response);
        }
        user.Username= userDto.Username;
        user.Password = userDto.Password;
        user.Email = userDto.Email;
        user.UpdatedBy = "";
        user.UpdatedDate = DateTime.Now;
        await _userRepository.UpdateAsync(user);
        response.Data = userDto;
        return Ok(response);
    }
    
    [HttpGet("exists/{username}")]
    public async Task<ActionResult> Exists(string username)
    {
        bool exists = await _userRepository.ExistsAsync(username);
        return Ok(exists);
    }
}