using Newtonsoft.Json;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Services;

public class Subject_GradeService : ISubject_GradeService
{
    private readonly string _baseUrl = "http://localhost:5279/";
    private readonly string _endpoint = "api/Subjects_Grades";
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public Subject_GradeService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    private HttpClient CreateHttpClient()
    {
        var client = new HttpClient();
        var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";

        // Agrega un header personalizado con el nombre del usuario
        client.DefaultRequestHeaders.Add("X-User", userName);

        return client;
    }
    
    public async Task<Response<List<Subject_GradeDto>>> GetAllAsync()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = CreateHttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<Subject_GradeDto>>>(json);
        
        return response;
    }

    public async Task<Response<Subject_GradeDto>> GetByIdAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client = CreateHttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<Subject_GradeDto>>(json);
        
        return response;
    }

    public async Task<Response<Subject_GradeDto>> SaveAsync(Subject_GradeDto subjectGrade)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(subjectGrade);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = CreateHttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<Subject_GradeDto>>(json);
        
        return response;
    }

    public async Task<Response<Subject_GradeDto>> UpdateAsync(Subject_GradeDto subjectGrade)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(subjectGrade);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = CreateHttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<Subject_GradeDto>>(json);

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
       
        var client = CreateHttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        return response;
    }
}