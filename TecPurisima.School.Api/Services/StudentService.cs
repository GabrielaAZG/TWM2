using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class StudentService: IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGroupRepository _groupRepository;
    
    public StudentService(IStudentRepository studentRepository, IGroupRepository groupRepository)
    {
        _studentRepository = studentRepository;
        _groupRepository = groupRepository;
    }
    
    public async Task<bool> StudentExist(int id)
    {
        var student = await _studentRepository.GetById(id);
        return (student != null);//valida que la marca no sea nula
    }

    public async Task<StudentDto> SaveAsync(StudentDto studentDto)
    {
        //AGREGADO
        if (!await _groupRepository.ExistsAsync(studentDto.GroupId))
            throw new Exception("School Group does not exist.");
        //
        
        var student = new Student
        {
            FullName = studentDto.FullName,
            Age = studentDto.Age,
            Gender = studentDto.Gender,
            CURP = studentDto.CURP,
            GroupId = studentDto.GroupId,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        student = await _studentRepository.SaveAsync(student);
        student.Id = student.Id;
        return studentDto;
    }

    public async Task<StudentDto> UpdateAsync(StudentDto studentDto)
    {
        var student = await _studentRepository.GetById(studentDto.Id);
        if (student == null)
        {
            throw new Exception("Student not found");
        }
        //AGREGADO
        if (!await _groupRepository.ExistsAsync(studentDto.GroupId))
            throw new Exception("School Group does not exist.");
        //
        student.FullName = studentDto.FullName;
        student.Age = studentDto.Age;
        student.Gender = studentDto.Gender;
        student.CURP = studentDto.CURP;
        student.GroupId = studentDto.GroupId;
        student.UpdatedBy = "";
        student.UpdatedDate = DateTime.Now; 
        await _studentRepository.UpdateAsync(student);
      
        return studentDto;
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        var studentsDto = students.Select(c => new StudentDto(c)).ToList();
        return studentsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _studentRepository.DeleteAsync(id);
    }

    public async Task<StudentDto> GetById(int id)
    {
        var student = await _studentRepository.GetById(id);

        if (student == null)
        {
            throw new Exception("Student not found");
        }
        var studentDto = new StudentDto(student);
        return studentDto;
    }
}