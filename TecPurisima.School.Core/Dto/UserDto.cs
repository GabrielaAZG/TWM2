using TecPurisima.School.Core.Entities;
namespace TecPurisima.School.Core.Dto;

public class UserDto: DtoBase
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public UserDto()
    {
        
    }
    
    public UserDto(User user)
    {
        Id = user.Id;
        Username = user.Username;
        Password = user.Password;
        Email = user.Email;
    }
}