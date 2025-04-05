namespace TecPurisima.School.Core.Entities;

public class Teacher: EntityBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string CURP { get; set; }
}