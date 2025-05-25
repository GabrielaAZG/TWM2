using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class AdminService: IAdminService
{
    private readonly IAdminRepository _adminRepository;
    
    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    
    
    
    public async Task<bool> AdminExist(int id)
    {
        var admin = await _adminRepository.GetById(id);
        return (admin != null);//valida que la marca no sea nula
    }

    public async Task<AdminDto> SaveAsync(AdminDto adminDto)
    {
        var admin = new Admin
        {
            FullName = adminDto.FullName,
            User = adminDto.User,
            Password = adminDto.Password,
            Email = adminDto.Email,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        admin = await _adminRepository.SaveAsync(admin);
        admin.Id = admin.Id;
        return adminDto;
    }

    public async Task<AdminDto> UpdateAsync(AdminDto adminDto)
    {
        var admin = await _adminRepository.GetById(adminDto.Id);
        if (admin == null)
        {
            throw new Exception("Admin not found");
        }
        admin.FullName = adminDto.FullName;
        admin.User = adminDto.User;
        admin.Password = adminDto.Password;
        admin.Email = adminDto.Email;
        admin.UpdatedDate = DateTime.Now;
        
        await _adminRepository.UpdateAsync(admin);
      
        return adminDto;
    }

    public async Task<List<AdminDto>> GetAllAsync()
    {
        var admins = await _adminRepository.GetAllAsync();
        var adminsDto = admins.Select(c => new AdminDto(c)).ToList();
        return adminsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _adminRepository.DeleteAsync(id);
    }

    public async Task<AdminDto> GetById(int id)
    {
        var admin = await _adminRepository.GetById(id);

        if (admin == null)
        {
            throw new Exception("Admin not found");
        }
        var adminDto = new AdminDto(admin);
        return adminDto;
    }
    
    public async Task<Admin?> LoginAsync(string username, string password)
    {
        var admin = await _adminRepository.GetByUsernameAsync(username);

        if (admin == null || admin.Password != password) 
            return null;

        return admin;
    }
}