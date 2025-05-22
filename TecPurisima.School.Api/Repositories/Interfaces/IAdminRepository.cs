using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface IAdminRepository
{
    //Metodo para guardar los alumnos
    Task<Admin> SaveAsync(Admin admin);
    
    //Metodo para actualizar la info de los alumnos
    Task<Admin> UpdateAsync(Admin admin);
    
    //Metodo para obtener los alumnos
    Task<List<Admin>> GetAllAsync();
    
    //Metodo para retornar el id de los alumnos que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un alumno por el id
    Task<Admin> GetById(int id);
    Task<Admin> GetByUsernameAsync(string username);
    Task CreateAsync(Admin admin);
}