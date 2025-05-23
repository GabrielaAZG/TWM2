using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface ISubjectService
{
    Task<Response<List<SubjectDto>>?> GetAllAsync();
    
    Task<Response<SubjectDto>> GetByIdAsync(int id);
    
    Task<Response<SubjectDto>> SaveAsync(SubjectDto student);
    
    Task<Response<SubjectDto>> UpdateAsync(SubjectDto student);
    
    Task<Response<bool>> DeleteAsync(int id);
}