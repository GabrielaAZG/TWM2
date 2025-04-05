using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface IStudentRepository
{
    //Metodo para guardar los alumnos
    Task<Student> SaveAsync(Student student);
    
    //Metodo para actualizar la info de los alumnos
    Task<Student> UpdateAsync(Student student);
    
    //Metodo para obtener los alumnos
    Task<List<Student>> GetAllAsync();
    
    //Metodo para retornar el id de los alumnos que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un alumno por el id
    Task<Student> GetById(int id);
}