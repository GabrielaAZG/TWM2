using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class SubjectRepository: ISubjectRepository
{
    private readonly IDbContext _dbContext;

    public SubjectRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    public async Task<Subject> SaveAsync(Subject subject)
    {
        subject.Id = await _dbContext.Connection.InsertAsync(subject);
        return subject;
    }

    public async Task<Subject> UpdateAsync(Subject subject)
    {
        await _dbContext.Connection.UpdateAsync(subject);
        return subject;
        
    }

    public async Task<List<Subject>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Subject WHERE IsDeleted = 0";
        var subjects = await _dbContext.Connection.QueryAsync<Subject>(sql);
        return subjects.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var subject = await GetById(id);
        if (subject == null)
        {
            return false;
        }
        subject.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(subject);
    }

    public async Task<Subject> GetById(int id)
    {
        var subject = await _dbContext.Connection.GetAsync<Subject>(id);
        if (subject == null)
        {
            return null;
        }
        return subject.IsDeleted == true ? null : subject;
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        var sql = "SELECT COUNT(1) FROM Subject WHERE Id = @Id AND IsDeleted = 0";
        var count = await _dbContext.Connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return count > 0;
    }
    
}