using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface IGroupService
{
    Task<bool> SchoolGroupExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<SchoolGroupDto> SaveAsync(SchoolGroupDto group);
    
    //Metodo para actualizar las marcas de un producto
    Task<SchoolGroupDto> UpdateAsync(SchoolGroupDto group);
    
    //Metodo para guardar las marcas de un producto
    Task<List<SchoolGroupDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<SchoolGroupDto> GetById(int id);
}