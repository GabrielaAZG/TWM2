using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories.Interfaces;

public interface IUserRepository
{
    //Metodo para guardar los usuario
    Task<User> SaveAsync(User user);
    
    //Metodo para actualizar los usuarios
    Task<User> UpdateAsync(User user);
    
    //Metodo para mostrar todos los usuarios
    Task<List<User>> GetAllAsync();
    
    //Metodo para retornar el id del usuario que se borrara
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener un usuario por el id
    Task<User> GetById(int id);
    
    //Metodo para validar usuario
    Task<bool> ExistsAsync(string username);
}