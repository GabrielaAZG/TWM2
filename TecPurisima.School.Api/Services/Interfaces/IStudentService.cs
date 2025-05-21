using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface IStudentService
{
    Task<bool> StudentExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<StudentDto> SaveAsync(StudentDto student);
    
    //Metodo para actualizar las marcas de un producto
    Task<StudentDto> UpdateAsync(StudentDto student);
    
    //Metodo para guardar las marcas de un producto
    Task<List<StudentDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<StudentDto> GetById(int id);
    
}