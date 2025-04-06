using Dapper;
using Dapper.Contrib.Extensions;
using TecPurisima.School.Api.DataAccess.Interfaces;
using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Repositories;

public class GroupTeacherSubjectRepository:IGroupTeacherSubject
{
    private readonly IDbContext _dbContext;
    public GroupTeacherSubjectRepository(IDbContext context)//Constructor
    {
        _dbContext = context;
    }
    public async Task<Group_Teacher_Subject> SaveAsync(Group_Teacher_Subject groupTeacherSubject)
    {
        groupTeacherSubject.Id = await _dbContext.Connection.InsertAsync(groupTeacherSubject);
        return groupTeacherSubject;
    }

    public async Task<Group_Teacher_Subject> UpdateAsync(Group_Teacher_Subject groupTeacherSubject)
    {
        await _dbContext.Connection.UpdateAsync(groupTeacherSubject);
        return groupTeacherSubject;
    }

    public async Task<List<Group_Teacher_Subject>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Student WHERE IsDeleted = 0";
        var groupTS = await _dbContext.Connection.QueryAsync<Group_Teacher_Subject>(sql);
        return groupTS.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var groupts = await GetById(id);
        if (groupts == null)
        {
            return false;
        }
        groupts.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(groupts);
    }

    public async Task<Group_Teacher_Subject> GetById(int id)
    {
        var groupts = await _dbContext.Connection.GetAsync<Group_Teacher_Subject>(id);
        if (groupts == null)
        {
            return null;
        }
        return groupts.IsDeleted == true ? null : groupts;
    }
}