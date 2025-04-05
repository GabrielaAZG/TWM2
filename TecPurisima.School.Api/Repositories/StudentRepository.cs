using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class StudentRepository: IStudentRepository
{
    private readonly IDbContext _dbContext;
    
    public StudentRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    
    
    public async Task<Student> SaveAsync(Student student)
    {
        student.Id = await _dbContext.Connection.InsertAsync(student);
        return student;
    }

    public async Task<Student> UpdateAsync(Student student)
    {
        await _dbContext.Connection.UpdateAsync(student);
        return student;
        
    }

    public async Task<List<Student>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Student WHERE IsDeleted = 0";
        var students = await _dbContext.Connection.QueryAsync<Student>(sql);
        return students.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await GetById(id);
        if (student == null)
        {
            return false;
        }
        student.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(student);
    }

    public async Task<Student> GetById(int id)
    {
        var student = await _dbContext.Connection.GetAsync<Student>(id);
        if (student == null)
        {
            return null;
        }
        return student.IsDeleted == true ? null : student;
    }
}