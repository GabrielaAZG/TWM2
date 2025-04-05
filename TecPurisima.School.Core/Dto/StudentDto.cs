using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class StudentDto: DtoBase
{
    public string FullName { get; set; }
    public string Genre { get; set; }
    public string CURP { get; set; }
    public int GroupId { get; set; }
    
    public StudentDto()
    {
        
    }
    
    public StudentDto(Student student)
    {
        Id = student.Id;
        FullName = student.FullName;
        Genre = student.Genre;
        CURP = student.CURP;
        GroupId = student.GroupId;
    }
}