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
        return admins.ToList();
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
}