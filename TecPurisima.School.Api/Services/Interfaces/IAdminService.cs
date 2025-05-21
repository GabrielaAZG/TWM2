using TecPurisima.School.Core.Dto;

namespace TecPurisima.School.Api.Services.Interfaces;

public interface IAdminService
{
    Task<bool> AdminExist(int id);
    
    //Metodo para guardar las marcas de un producto
    Task<AdminDto> SaveAsync(AdminDto admin);
    
    //Metodo para actualizar las marcas de un producto
    Task<AdminDto> UpdateAsync(AdminDto admin);
    
    //Metodo para guardar las marcas de un producto
    Task<List<AdminDto>> GetAllAsync();
    
    //Metodo para retornar el id de las marcas de producto que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una marca por el id
    Task<AdminDto> GetById(int id);
    
}