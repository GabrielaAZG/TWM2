using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface IGradeService
{
    Task<Response<List<GradeDto>>> GetAllAsync();
    
    Task<Response<GradeDto>> GetByIdAsync(int id);
    
    Task<Response<GradeDto>> SaveAsync(GradeDto grade);
    
    Task<Response<GradeDto>> UpdateAsync(GradeDto grade);
    
    Task<Response<bool>> DeleteAsync(int id);
}