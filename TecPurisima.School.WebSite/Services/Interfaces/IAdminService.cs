using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;

namespace TecPurisima.School.WebSite.Services.Interfaces;

public interface IAdminService
{
    Task<Response<List<AdminDto>>> GetAllAsync();
    
    Task<Response<AdminDto>> GetByIdAsync(int id);
    
    Task<Response<AdminDto>> SaveAsync(AdminDto admin);
    
    Task<Response<AdminDto>> UpdateAsync(AdminDto admin);
    
    Task<Response<bool>> DeleteAsync(int id);
}