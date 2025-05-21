using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class GroupService: IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly ITeacherRepository _teacherRepository;
    
    public GroupService(IGroupRepository groupRepository, IGradeRepository gradeRepository, ITeacherRepository teacherRepository )
    {
        _groupRepository = groupRepository;
        _gradeRepository = gradeRepository;
        _teacherRepository = teacherRepository;
    }
    
    public async Task<bool> SchoolGroupExist(int id)
    {
        var group = await _groupRepository.GetById(id);
        return (group != null);//valida que la marca no sea nula
    }

    public async Task<SchoolGroupDto> SaveAsync(SchoolGroupDto groupDto)
    {
        //AGREGADO
        if (!await _gradeRepository.ExistsAsync(groupDto.GradeId))
            throw new Exception("Grade does not exist.");

        if (!await _teacherRepository.ExistsAsync(groupDto.TeacherId))
            throw new Exception("Teacher does not exist.");
        //
        var group = new SchoolGroup
        {
            GroupName = groupDto.GroupName,
            GradeId = groupDto.GradeId,
            TeacherId = groupDto.TeacherId,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        group = await _groupRepository.SaveAsync(group);
        group.Id = group.Id;
        return groupDto;
    }

    public async Task<SchoolGroupDto> UpdateAsync(SchoolGroupDto groupDto)
    {
        var group = await _groupRepository.GetById(groupDto.Id);
        if (group == null)
        {
            throw new Exception("School Group not found");
        }
        
        //AGREGADO
        if (!await _gradeRepository.ExistsAsync(groupDto.GradeId))
            throw new Exception("Grade does not exist.");

        if (!await _teacherRepository.ExistsAsync(groupDto.TeacherId))
            throw new Exception("Teacher does not exist.");
        //
        
        group.GroupName = groupDto.GroupName;
        group.GradeId = groupDto.GradeId;
        group.TeacherId = groupDto.TeacherId;
        group.UpdatedBy = "";
        group.UpdatedDate = DateTime.Now; 
        await _groupRepository.UpdateAsync(group);
      
        return groupDto;
    }

    public async Task<List<SchoolGroupDto>> GetAllAsync()
    {
        var groups = await _groupRepository.GetAllAsync();
        var groupsDto = groups.Select(c => new SchoolGroupDto(c)).ToList();
        return groupsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _groupRepository.DeleteAsync(id);
    }

    public async Task<SchoolGroupDto> GetById(int id)
    {
        var group = await _groupRepository.GetById(id);

        if (group == null)
        {
            throw new Exception("School Group not found");
        }
        var groupDto = new SchoolGroupDto(group);
        return groupDto;
    }
}