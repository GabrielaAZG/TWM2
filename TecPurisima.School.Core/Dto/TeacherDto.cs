using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class TeacherDto: DtoBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string CURP { get; set; }
    public string Gender { get; set; }
    
    
    public TeacherDto()
    {
        
    }
    
    public TeacherDto(Teacher teacher)
    {
        Id= teacher.Id;
        FullName = teacher.FullName;
        Email = teacher.Email;
        Age = teacher.Age;
        CURP = teacher.CURP;
        Gender = teacher.Gender;
    }
    
}