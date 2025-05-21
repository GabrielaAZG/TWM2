using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface IGradeService
{
    Task<bool> GradeExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<GradeDto> SaveAsync(GradeDto grade);
    
    //Metodo para actualizar las marcas de un producto
    Task<GradeDto> UpdateAsync(GradeDto grade);
    
    //Metodo para guardar las marcas de un producto
    Task<List<GradeDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<GradeDto> GetById(int id);
    
}