using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Core.Dto;

public class SubjectDto : DtoBase
{
    public string SubjectName { get; set; }
    
    public SubjectDto()
    {
        
    }
    
    public SubjectDto(Subject subject)
    {
        Id = subject.Id;
        SubjectName = subject.SubjectName;
    }
}