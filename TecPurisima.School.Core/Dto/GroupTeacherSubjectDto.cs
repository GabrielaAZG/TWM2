using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class GroupTeacherSubjectDto: DtoBase
{
    public int GroupId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    
    public GroupTeacherSubjectDto()
    {
        
    }
    
    public GroupTeacherSubjectDto(GroupTeacherSubject groupTeacherSubject)
    {
        Id = groupTeacherSubject.Id;
        GroupId = groupTeacherSubject.GroupId;
        TeacherId = groupTeacherSubject.TeacherId;
        SubjectId = groupTeacherSubject.SubjectId;
    }
    
}