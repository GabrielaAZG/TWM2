using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class GradeDto: DtoBase
{
    public string SchoolGrade { get; set; }
    
    public GradeDto()
    {
        
    }
    
    public GradeDto(Grade grade)
    {
        Id = grade.Id;
        SchoolGrade = grade.SchoolGrade;
    }
}