using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface IStudentService
{
    Task<Response<List<StudentDto>>> GetAllAsync();
    
    Task<Response<StudentDto>> GetByIdAsync(int id);
    
    Task<Response<StudentDto>> SaveAsync(StudentDto student);
    
    Task<Response<StudentDto>> UpdateAsync(StudentDto student);
    
    Task<Response<bool>> DeleteAsync(int id);
}