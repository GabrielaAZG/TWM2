using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class AdminRepository: IAdminRepository
{
    private readonly IDbContext _dbContext;
    
    public AdminRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    
    public async Task<Admin> SaveAsync(Admin admin)
    {
        admin.Id = await _dbContext.Connection.InsertAsync(admin);
        return admin;
    }

    public async Task<Admin> UpdateAsync(Admin admin)
    {
        await _dbContext.Connection.UpdateAsync(admin);
        return admin;
        
    }

    public async Task<List<Admin>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Admin WHERE IsDeleted = 0";
        var admins = await _dbContext.Connection.QueryAsync<Admin>(sql);
        return admins.Select(a => new Admin
        {
            Id = a.Id,
            FullName = a.FullName,
            User = a.User,
            Email = a.Email,
            Password = a.Password,
            Role = a.Role,
        }).ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var admin = await GetById(id);
        if (admin == null)
        {
            return false;
        }
        admin.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(admin);
    }

    public async Task<Admin> GetById(int id)
    {
        var admin = await _dbContext.Connection.GetAsync<Admin>(id);
        if (admin == null)
        {
            return null;
        }
        return admin.IsDeleted == true ? null : admin;
    }
    
    public async Task<Admin> GetByUsernameAsync(string username)
    {
        var connection = _dbContext.Connection;
        const string query = "SELECT * FROM Admin WHERE User = @Username LIMIT 1";
        return await connection.QueryFirstOrDefaultAsync<Admin>(query, new { Username = username });
    }


    public async Task CreateAsync(Admin admin)
    {
        var connection = _dbContext.Connection;
        await connection.InsertAsync(admin);
    }
}