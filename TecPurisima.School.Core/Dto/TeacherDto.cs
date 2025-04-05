using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class TeacherDto: DtoBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Genre { get; set; }
    public string CURP { get; set; }
    
    public TeacherDto()
    {
        
    }
    
    public TeacherDto(Teacher teacher)
    {
        Id= teacher.Id;
        FullName = teacher.FullName;
        Email = teacher.Email;
        Genre = teacher.Genre;
        CURP = teacher.CURP;
    }
    
}