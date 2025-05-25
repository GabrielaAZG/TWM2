using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class TeacherService: ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TeacherService(ITeacherRepository teacherRepository, IHttpContextAccessor httpContextAccessor)
    {
        _teacherRepository = teacherRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private string GetCurrentUser()
    {
        var headers = _httpContextAccessor.HttpContext?.Request?.Headers;
        return headers != null && headers.ContainsKey("X-User") ? headers["X-User"].ToString() : "System";
    }
    
    public async Task<bool> TeacherExist(int id)
    {
        var teacher = await _teacherRepository.GetById(id);
        return (teacher != null);//valida que la marca no sea nula
    }

    public async Task<TeacherDto> SaveAsync(TeacherDto teacherDto)
    {
        var teacher = new Teacher
        {
            FullName = teacherDto.FullName,
            Email = teacherDto.Email,
            Age = teacherDto.Age,
            CURP = teacherDto.CURP,
            Gender = teacherDto.Gender,
            CreatedBy = GetCurrentUser(),
            CreatedDate = DateTime.Now,
            UpdatedBy = GetCurrentUser(),
            UpdatedDate = DateTime.Now
        };
        teacher = await _teacherRepository.SaveAsync(teacher);
        teacher.Id = teacher.Id;
        return teacherDto;
    }

    public async Task<TeacherDto> UpdateAsync(TeacherDto teacherDto)
    {
        var teacher = await _teacherRepository.GetById(teacherDto.Id);
        if (teacher == null)
        {
            throw new Exception("Teacher not found");
        }
        teacher.FullName = teacherDto.FullName;
        teacher.Email = teacherDto.Email;
        teacher.Age = teacherDto.Age;
        teacher.CURP = teacherDto.CURP;
        teacher.Gender = teacherDto.Gender;
        teacher.UpdatedBy = GetCurrentUser();
        teacher.UpdatedDate = DateTime.Now; 
        await _teacherRepository.UpdateAsync(teacher);
      
        return teacherDto;
    }

    public async Task<List<TeacherDto>> GetAllAsync()
    {
        var teachers = await _teacherRepository.GetAllAsync();
        var teachersDto = teachers.Select(c => new TeacherDto(c)).ToList();
        return teachersDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _teacherRepository.DeleteAsync(id);
    }

    public async Task<TeacherDto> GetById(int id)
    {
        var teacher = await _teacherRepository.GetById(id);

        if (teacher == null)
        {
            throw new Exception("Teacher not found");
        }
        var teacherDto = new TeacherDto(teacher);
        return teacherDto;
    }
}