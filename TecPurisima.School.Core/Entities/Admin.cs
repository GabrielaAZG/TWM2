namespace TecPurisima.School.Core.Entities;

public class Admin: EntityBaseWithoutAudit
{
    public string FullName { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } = "admin";
}