using Microsoft.AspNetCore.Mvc;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.Api.Controllers;

[ApiController]//Esto hace que la clase sea un endpoint
[Route("api/[controller]")]//Esta es la ruta: api/ProductBrands

public class AdminsController : ControllerBase
{
    private readonly IAdminService _adminService;
    
    public AdminsController(IAdminService adminService) //Constructor del controlador
    {
        
        _adminService = adminService;

    }
    
    
    [HttpGet] //Este método responde a solicitudes GET
    public async Task<ActionResult<Response<List<Admin>>>> GetAll() //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
        var response = new Response<List<AdminDto>>
        {
            Data = await _adminService.GetAllAsync(),
        };
        return Ok(response);
    }
    
    [HttpPost] //Este método responde a solicitudes POST
    public async Task<ActionResult<Response<AdminDto>>> Post([FromBody] AdminDto adminDto) //Metodo GetAll devuelve todas las marcas y devuelve un objeto Response que contiene la lista ProductBrand
    {
            var response = new Response<AdminDto>
            {
                Data = await _adminService.SaveAsync(adminDto)
            };

            return Created($"/api/[controller]/{response.Data.Id}", response);
        
    }

    [HttpGet] //Este método responde a solicitudes GET BY ID
    [Route("{id:int}")]
    public async Task<ActionResult<Response<AdminDto>>> GetById(int id)
    {
        var response = new Response<AdminDto>();
        
        if (!await _adminService.AdminExist(id))
        {
            response.Errors.Add(("Admin Not Found"));
            return NotFound(response);
        }
      
        response.Data = await _adminService.GetById(id);
        return Ok(response);
    }

    [HttpDelete] //Este método responde a solicitudes DELETE
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _adminService.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }

    [HttpPut] //Este método responde a solicitudes UPDATE
    public async Task<ActionResult<Response<AdminDto>>> Update([FromBody] AdminDto adminDto)
    {
        var response = new Response<AdminDto>();
        
        if (!await _adminService.AdminExist(adminDto.Id))
        {
            response.Errors.Add(("Admin Not Found"));
            return NotFound(response);
        }
        
        response.Data = await _adminService.UpdateAsync(adminDto);
        return Ok(response);
    }
}