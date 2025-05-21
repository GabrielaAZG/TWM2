namespace TecPurisima.School.Core.Entities;

public class SchoolGroup: EntityBase
{
    public string GroupName { get; set; }
    public int GradeId { get; set; }
    public int TeacherId { get; set; }
}
