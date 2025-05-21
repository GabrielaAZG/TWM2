using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class GradeRepository: IGradeRepository
{
    private readonly IDbContext _dbContext;
    
    public GradeRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    
    public async Task<Grade> SaveAsync(Grade grade)
    {
        grade.Id = await _dbContext.Connection.InsertAsync(grade);
        return grade;
    }

    public async Task<Grade> UpdateAsync(Grade grade)
    {
        await _dbContext.Connection.UpdateAsync(grade);
        return grade;
        
    }

    public async Task<List<Grade>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Grade WHERE IsDeleted = 0";
        var grades = await _dbContext.Connection.QueryAsync<Grade>(sql);
        return grades.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var grade = await GetById(id);
        if (grade == null)
        {
            return false;
        }
        grade.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(grade);
    }

    public async Task<Grade> GetById(int id)
    {
        var grade = await _dbContext.Connection.GetAsync<Grade>(id);
        if (grade == null)
        {
            return null;
        }
        return grade.IsDeleted == true ? null : grade;
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        var sql = "SELECT COUNT(1) FROM Grade WHERE Id = @Id AND IsDeleted = 0";
        var count = await _dbContext.Connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return count > 0;
    }
}