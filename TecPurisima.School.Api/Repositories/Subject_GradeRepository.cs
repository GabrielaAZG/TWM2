using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class Subject_GradeRepository: ISubject_GradeRepository
{
    private readonly IDbContext _dbContext;
    
    public Subject_GradeRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    
    public async Task<Subject_Grade> SaveAsync(Subject_Grade subjectgrade)
    {
        subjectgrade.Id = await _dbContext.Connection.InsertAsync(subjectgrade);
        return subjectgrade;
    }

    public async Task<Subject_Grade> UpdateAsync(Subject_Grade subjectgrade)
    {
        await _dbContext.Connection.UpdateAsync(subjectgrade);
        return subjectgrade;
        
    }

    public async Task<List<Subject_Grade>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Subject_Grade WHERE IsDeleted = 0";
        var subjectsgrades = await _dbContext.Connection.QueryAsync<Subject_Grade>(sql);
        return subjectsgrades.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var subjectgrade = await GetById(id);
        if (subjectgrade == null)
        {
            return false;
        }
        subjectgrade.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(subjectgrade);
    }

    public async Task<Subject_Grade> GetById(int id)
    {
        var subjectgrade = await _dbContext.Connection.GetAsync<Subject_Grade>(id);
        if (subjectgrade == null)
        {
            return null;
        }
        return subjectgrade.IsDeleted == true ? null : subjectgrade;
    }
}