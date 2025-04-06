using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface IGroupTeacherSubject
{
    //Metodo para guardar lar relaciones
    Task<Group_Teacher_Subject> SaveAsync(Group_Teacher_Subject groupTeacherSubject);
    
    //Metodo para actualizar la info de las relaciones
    Task<Group_Teacher_Subject> UpdateAsync(Group_Teacher_Subject groupTeacherSubject);
    
    //Metodo para obtener las relaciones
    Task<List<Group_Teacher_Subject>> GetAllAsync();
    
    //Metodo para retornar el id de las relaciones que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener las relaciones por el id
    Task<Group_Teacher_Subject> GetById(int id);
}