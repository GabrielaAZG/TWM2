using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface ISubject_GradeService
{
    Task<bool> Subject_GradeExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<Subject_GradeDto> SaveAsync(Subject_GradeDto subjectgrade);
    
    //Metodo para actualizar las marcas de un producto
    Task<Subject_GradeDto> UpdateAsync(Subject_GradeDto subjectgrade);
    
    //Metodo para guardar las marcas de un producto
    Task<List<Subject_GradeDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<Subject_GradeDto> GetById(int id);
    
}