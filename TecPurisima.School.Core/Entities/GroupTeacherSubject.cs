namespace TecPurisima.School.Core.Entities;

public class Group_Teacher_Subject: EntityBase
{
    
    public int GroupId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
}