using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface ISubject_GradeService
{
    Task<Response<List<Subject_GradeDto>>> GetAllAsync();
    
    Task<Response<Subject_GradeDto>> GetByIdAsync(int id);
    
    Task<Response<Subject_GradeDto>> SaveAsync(Subject_GradeDto subjectGrade);
    
    Task<Response<Subject_GradeDto>> UpdateAsync(Subject_GradeDto subjectGrade);
    
    Task<Response<bool>> DeleteAsync(int id);
}