using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface IGroupRepository
{
    //Metodo para guardar los grupos
    Task<SchoolGroup> SaveAsync(SchoolGroup group);
    
    //Metodo para actualizar los grupos
    Task<SchoolGroup> UpdateAsync(SchoolGroup group);
    
    //Metodo para mostrar todos los grupos
    Task<List<SchoolGroup>> GetAllAsync();
    
    //Metodo para retornar el id del grupo que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un grupo por el id
    Task<SchoolGroup> GetById(int id);
}