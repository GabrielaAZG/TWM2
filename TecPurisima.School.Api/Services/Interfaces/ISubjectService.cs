using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface ISubjectService
{
    Task<bool> SubjectExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<SubjectDto> SaveAsync(SubjectDto subject);
    
    //Metodo para actualizar las marcas de un producto
    Task<SubjectDto> UpdateAsync(SubjectDto subject);
    
    //Metodo para guardar las marcas de un producto
    Task<List<SubjectDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<SubjectDto> GetById(int id);
    
}