using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface ISubjectRepository
{
    //Metodo para guardar las materias
    Task<Subject> SaveAsync(Subject subject);
    
    //Metodo para actualizar las materias
    Task<Subject> UpdateAsync(Subject subject);
    
    //Metodo para mostrar todas las materias
    Task<List<Subject>> GetAllAsync();
    
    //Metodo para retornar el id de la materia que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una materia por el id
    Task<Subject> GetById(int id);

    Task<bool> ExistsAsync(int id);
}