using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface ITeacherService
{
    Task<Response<List<TeacherDto>>> GetAllAsync();
    
    Task<Response<TeacherDto>> GetByIdAsync(int id);
    
    Task<Response<TeacherDto>> SaveAsync(TeacherDto student);
    
    Task<Response<TeacherDto>> UpdateAsync(TeacherDto student);
    
    Task<Response<bool>> DeleteAsync(int id);
}