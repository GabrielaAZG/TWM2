using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface IGradeRepository
{
    //Metodo para guardar los grupos
    Task<Grade> SaveAsync(Grade grade);
    
    //Metodo para actualizar los grupos
    Task<Grade> UpdateAsync(Grade grade);
    
    //Metodo para mostrar todos los grupos
    Task<List<Grade>> GetAllAsync();
    
    //Metodo para retornar el id del grupo que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un grupo por el id
    Task<Grade> GetById(int id);

    Task<bool> ExistsAsync(int id);
}