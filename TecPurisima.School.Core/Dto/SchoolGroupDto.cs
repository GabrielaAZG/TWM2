using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class SchoolGroupDto: DtoBase
{
    public string GroupName { get; set; }
    public int GradeId { get; set; }
    public int TeacherId { get; set; }
    
    public SchoolGroupDto()
    {
        
    }
    
    public SchoolGroupDto(SchoolGroup schoolGroup)
    {
        Id = schoolGroup.Id;
        GroupName = schoolGroup.GroupName;
        GradeId = schoolGroup.GradeId;
        TeacherId = schoolGroup.TeacherId;

    }
}