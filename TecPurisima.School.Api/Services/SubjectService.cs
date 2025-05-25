using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class SubjectService: ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public SubjectService(ISubjectRepository subjectRepository, IHttpContextAccessor httpContextAccessor)
    {
        _subjectRepository = subjectRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    private string GetCurrentUser()
    {
        var headers = _httpContextAccessor.HttpContext?.Request?.Headers;
        return headers != null && headers.ContainsKey("X-User") ? headers["X-User"].ToString() : "System";
    }
    
    public async Task<bool> SubjectExist(int id)
    {
        var subject = await _subjectRepository.GetById(id);
        return (subject != null);//valida que la marca no sea nula
    }

    public async Task<SubjectDto> SaveAsync(SubjectDto subjectDto)
    {
        var subject = new Subject
        {
            SubjectName = subjectDto.SubjectName,
            CreatedBy = GetCurrentUser(),
            CreatedDate = DateTime.Now,
            UpdatedBy = GetCurrentUser(),
            UpdatedDate = DateTime.Now
        };
        subject = await _subjectRepository.SaveAsync(subject);
        subject.Id = subject.Id;
        return subjectDto;
    }

    public async Task<SubjectDto> UpdateAsync(SubjectDto subjectDto)
    {
        var subject = await _subjectRepository.GetById(subjectDto.Id);
        if (subject == null)
        {
            throw new Exception("Subject not found");
        }
        subject.SubjectName = subjectDto.SubjectName;
        subject.UpdatedBy = GetCurrentUser();
        subject.UpdatedDate = DateTime.Now; 
        await _subjectRepository.UpdateAsync(subject);
      
        return subjectDto;
    }

    public async Task<List<SubjectDto>> GetAllAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        var subjectsDto = subjects.Select(c => new SubjectDto(c)).ToList();
        return subjectsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _subjectRepository.DeleteAsync(id);
    }

    public async Task<SubjectDto> GetById(int id)
    {
        var subject = await _subjectRepository.GetById(id);

        if (subject == null)
        {
            throw new Exception("Subject not found");
        }
        var subjectDto = new SubjectDto(subject);
        return subjectDto;
    }
}