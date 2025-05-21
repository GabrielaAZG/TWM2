using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface ITeacherService
{
    Task<bool> TeacherExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<TeacherDto> SaveAsync(TeacherDto brand);
    
    //Metodo para actualizar las marcas de un producto
    Task<TeacherDto> UpdateAsync(TeacherDto brand);
    
    //Metodo para guardar las marcas de un producto
    Task<List<TeacherDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<TeacherDto> GetById(int id);
    
}