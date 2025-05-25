using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface IGroupService
{
    Task<Response<List<SchoolGroupDto>>> GetAllAsync();
    
    Task<Response<SchoolGroupDto>> GetByIdAsync(int id);
    
    Task<Response<SchoolGroupDto>> SaveAsync(SchoolGroupDto group);
    
    Task<Response<SchoolGroupDto>> UpdateAsync(SchoolGroupDto group);
    
    Task<Response<bool>> DeleteAsync(int id);
}