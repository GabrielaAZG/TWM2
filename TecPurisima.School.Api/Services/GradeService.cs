using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class GradeService: IGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public GradeService(IGradeRepository gradeRepository, IHttpContextAccessor httpContextAccessor)
    {
        _gradeRepository = gradeRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private string GetCurrentUser()
    {
        var headers = _httpContextAccessor.HttpContext?.Request?.Headers;
        return headers != null && headers.ContainsKey("X-User") ? headers["X-User"].ToString() : "System";
    }
    
    public async Task<bool> GradeExist(int id)
    {
        var grade = await _gradeRepository.GetById(id);
        return (grade != null);//valida que la marca no sea nula
    }

    public async Task<GradeDto> SaveAsync(GradeDto gradeDto)
    {
        var grade = new Grade
        {
            SchoolGrade = gradeDto.SchoolGrade,
            CreatedBy = GetCurrentUser(),
            CreatedDate = DateTime.Now,
            UpdatedBy = GetCurrentUser(),
            UpdatedDate = DateTime.Now
        };
        grade = await _gradeRepository.SaveAsync(grade);
        grade.Id = grade.Id;
        return gradeDto;
    }

    public async Task<GradeDto> UpdateAsync(GradeDto gradeDto)
    {
        var grade = await _gradeRepository.GetById(gradeDto.Id);
        if (grade == null)
        {
            throw new Exception("School Grade not found");
        }
        grade.SchoolGrade = gradeDto.SchoolGrade;
        grade.UpdatedBy = GetCurrentUser();
        grade.UpdatedDate = DateTime.Now;
        
        await _gradeRepository.UpdateAsync(grade);
      
        return gradeDto;
    }

    public async Task<List<GradeDto>> GetAllAsync()
    {
        var grades = await _gradeRepository.GetAllAsync();
        var gradesDto = grades.Select(c => new GradeDto(c)).ToList();
        return gradesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _gradeRepository.DeleteAsync(id);
    }

    public async Task<GradeDto> GetById(int id)
    {
        var grade = await _gradeRepository.GetById(id);

        if (grade == null)
        {
            throw new Exception("School Grade not found");
        }
        var gradeDto = new GradeDto(grade);
        return gradeDto;
    }
}