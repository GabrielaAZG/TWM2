using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface ISubject_GradeRepository
{
    //Metodo para guardar los grupos
    Task<Subject_Grade> SaveAsync(Subject_Grade subjectgrade);
    
    //Metodo para actualizar los grupos
    Task<Subject_Grade> UpdateAsync(Subject_Grade subjectgrade);
    
    //Metodo para mostrar todos los grupos
    Task<List<Subject_Grade>> GetAllAsync();
    
    //Metodo para retornar el id del grupo que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un grupo por el id
    Task<Subject_Grade> GetById(int id);

    //Task<bool> ExistsAsync(int id);
}