using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface ITeacherRepository
{
    //Metodo para guardar los maestros
    Task<Teacher> SaveAsync(Teacher teacher);
    
    //Metodo para actualizar los maestros
    Task<Teacher> UpdateAsync(Teacher teacher);
    
    //Metodo para mostrar todos los maestros
    Task<List<Teacher>> GetAllAsync();
    
    //Metodo para retornar el id del maestro que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un maestro por el id
    Task<Teacher> GetById(int id);
}