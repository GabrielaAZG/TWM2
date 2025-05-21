using TecPurisima.School.Api.Repositories.Interfaces;
using TecPurisima.School.Api.Services.Interfaces;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Entities;

namespace TecPurisima.School.Api.Services;

public class Subject_GradeService: ISubject_GradeService
{
    private readonly ISubject_GradeRepository _subjectgradeRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly ISubjectRepository _subjectRepository;
    
    public Subject_GradeService(ISubject_GradeRepository subjectgradeRepository, IGradeRepository gradeRepository, ISubjectRepository subjectRepository)
    {
        _subjectgradeRepository = subjectgradeRepository;
        _gradeRepository = gradeRepository;
        _subjectRepository = subjectRepository;
    }
    
    public async Task<bool> Subject_GradeExist(int id)
    {
        var subjectgrade = await _subjectgradeRepository.GetById(id);
        return (subjectgrade != null);//valida que la marca no sea nula
    }

    public async Task<Subject_GradeDto> SaveAsync(Subject_GradeDto subjectgradeDto)
    {
        //AGREGADO
        if (!await _gradeRepository.ExistsAsync(subjectgradeDto.GradeId))
            throw new Exception("Grade does not exist.");

        if (!await _subjectRepository.ExistsAsync(subjectgradeDto.SubjectId))
            throw new Exception("Subject does not exist.");
        //
        var subjectgrade = new Subject_Grade
        {
            GradeId = subjectgradeDto.GradeId,
            SubjectId = subjectgradeDto.SubjectId,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        subjectgrade = await _subjectgradeRepository.SaveAsync(subjectgrade);
        subjectgrade.Id = subjectgrade.Id;
        return subjectgradeDto;
    }

    public async Task<Subject_GradeDto> UpdateAsync(Subject_GradeDto subjectgradeDto)
    {
        var subjectgrade = await _subjectgradeRepository.GetById(subjectgradeDto.Id);
        if (subjectgrade == null)
        {
            throw new Exception("Subject and Grade not found");
        }
        
        //AGREGADO
        if (!await _gradeRepository.ExistsAsync(subjectgradeDto.GradeId))
            throw new Exception("Grade does not exist.");

        if (!await _subjectRepository.ExistsAsync(subjectgradeDto.SubjectId))
            throw new Exception("Subject does not exist.");
        //
        
        subjectgrade.GradeId = subjectgradeDto.GradeId;
        subjectgrade.SubjectId = subjectgradeDto.SubjectId;
        subjectgrade.UpdatedBy = "";
        subjectgrade.UpdatedDate = DateTime.Now; 
        await _subjectgradeRepository.UpdateAsync(subjectgrade);
      
        return subjectgradeDto;
    }

    public async Task<List<Subject_GradeDto>> GetAllAsync()
    {
        var subjectsgrades = await _subjectgradeRepository.GetAllAsync();
        var subjectsgradesDto = subjectsgrades.Select(c => new Subject_GradeDto(c)).ToList();
        return subjectsgradesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _subjectgradeRepository.DeleteAsync(id);
    }

    public async Task<Subject_GradeDto> GetById(int id)
    {
        var subjectgrade = await _subjectgradeRepository.GetById(id);

        if (subjectgrade == null)
        {
            throw new Exception("Subject and Grade not found");
        }
        var subjectgradeDto = new Subject_GradeDto(subjectgrade);
        return subjectgradeDto;
    }
}