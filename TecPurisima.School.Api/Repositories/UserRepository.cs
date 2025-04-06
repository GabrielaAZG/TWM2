using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class UserRepository: IUserRepository
{
    private readonly IDbContext _dbContext;

    public UserRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    public async Task<User> SaveAsync(User user)
    {
        user.Id = await _dbContext.Connection.InsertAsync(user);
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        await _dbContext.Connection.UpdateAsync(user);
        return user;
        
    }

    public async Task<List<User>> GetAllAsync()
    {
        const string sql = "SELECT * FROM User WHERE IsDeleted = 0";
        var users = await _dbContext.Connection.QueryAsync<User>(sql);
        return users.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetById(id);
        if (user == null)
        {
            return false;
        }
        user.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(user);
    }

    public async Task<User> GetById(int id)
    {
        var user = await _dbContext.Connection.GetAsync<User>(id);
        if (user == null)
        {
            return null;
        }
        return user.IsDeleted == true ? null : user;
    }

    public async Task<bool> ExistsAsync(string username)
    {
        const string sql = "SELECT * FROM User WHERE Username = @Username  AND IsDeleted = 0";
        var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        return result != null;
    }
}