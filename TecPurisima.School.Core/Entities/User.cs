namespace TecPurisima.School.Core.Entities;

public class User: EntityBase
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int StudentId { get; set; }
    
}