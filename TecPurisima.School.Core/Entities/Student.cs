namespace TecPurisima.School.Core.Entities;

public class Student: EntityBase
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string CURP { get; set; }
    public int GroupId { get; set; }
}