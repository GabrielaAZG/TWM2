using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class AdminDto: DtoBase
{
    public string FullName { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public AdminDto()
    {
        
    }
    
    public AdminDto(Admin admin)
    {
        Id = admin.Id;
        FullName = admin.FullName;
        User = admin.User;
        Password = admin.Password;
        Email = admin.Email;
    }
}