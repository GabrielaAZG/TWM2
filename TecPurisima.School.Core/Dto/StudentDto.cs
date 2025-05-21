using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class StudentDto: DtoBase
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string CURP { get; set; }
    public int GroupId { get; set; }
    
    public StudentDto()
    {
        
    }
    
    public StudentDto(Student student)
    {
        Id = student.Id;
        FullName = student.FullName;
        Age = student.Age;
        Gender = student.Gender;
        CURP = student.CURP;
        GroupId = student.GroupId;
    }
}