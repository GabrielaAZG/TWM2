using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class TeacherRepository: ITeacherRepository
{
    private readonly IDbContext _dbContext;

    public TeacherRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    public async Task<Teacher> SaveAsync(Teacher teacher)
    {
        teacher.Id = await _dbContext.Connection.InsertAsync(teacher);
        return teacher;
    }

    public async Task<Teacher> UpdateAsync(Teacher teacher)
    {
        await _dbContext.Connection.UpdateAsync(teacher);
        return teacher;
        
    }

    public async Task<List<Teacher>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Teacher WHERE IsDeleted = 0";
        var teachers = await _dbContext.Connection.QueryAsync<Teacher>(sql);
        return teachers.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var teacher = await GetById(id);
        if (teacher == null)
        {
            return false;
        }
        teacher.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(teacher);
    }

    public async Task<Teacher> GetById(int id)
    {
        var teacher = await _dbContext.Connection.GetAsync<Teacher>(id);
        if (teacher == null)
        {
            return null;
        }
        return teacher.IsDeleted == true ? null : teacher;
    }
    
}