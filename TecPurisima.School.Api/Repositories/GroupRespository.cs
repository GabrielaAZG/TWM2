using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class GroupRespository : IGroupRepository
{
    private readonly IDbContext _dbContext;
    public GroupRespository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    public async Task<SchoolGroup> SaveAsync(SchoolGroup group)
    {
        group.Id = await _dbContext.Connection.InsertAsync(group);
        return group;
    }

    public async Task<SchoolGroup> UpdateAsync(SchoolGroup group)
    {
        await _dbContext.Connection.UpdateAsync(group);
        return group;
    }

    public async Task<List<SchoolGroup>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Teacher WHERE IsDeleted = 0";
        var groups = await _dbContext.Connection.QueryAsync<SchoolGroup>(sql);
        return groups.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var group = await GetById(id);
        if (group == null)
        {
            return false;
        }
        group.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(group);
    }

    public async Task<SchoolGroup> GetById(int id)
    {
        var group = await _dbContext.Connection.GetAsync<SchoolGroup>(id);
        if (group == null)
        {
            return null;
        }
        return group.IsDeleted == true ? null : group;
    }
}