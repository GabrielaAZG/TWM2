using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class Subject_GradeDto: DtoBase
{
    public int GradeId { get; set; }
    public int SubjectId { get; set; }
    
    public Subject_GradeDto()
    {
        
    }
    
    public Subject_GradeDto(Subject_Grade subject_grade)
    {
        Id = subject_grade.Id;
        GradeId = subject_grade.GradeId;
        SubjectId = subject_grade.SubjectId;
    }
    
}