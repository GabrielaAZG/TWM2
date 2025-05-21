namespace TecPurisima.School.Core.Entities;

public class EntityBaseWithoutAudit
{
    public int Id { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; } 
    public DateTime UpdatedDate { get; set; }
}