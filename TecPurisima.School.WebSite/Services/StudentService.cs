using Newtonsoft.Json;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Services;

public class StudentService : IStudentService
{
    private readonly string _baseUrl = "http://localhost:5279/";
    private readonly string _endpoint = "api/Students";
    
    public async Task<Response<List<StudentDto>>> GetAllAsync()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<StudentDto>>>(json);
        
        return response;
    }

    public async Task<Response<StudentDto>> GetByIdAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<StudentDto>>(json);
        
        return response;
    }

    public async Task<Response<StudentDto>> SaveAsync(StudentDto student)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(student);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<StudentDto>>(json);
        
        return response;
    }

    public async Task<Response<StudentDto>> UpdateAsync(StudentDto student)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(student);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<StudentDto>>(json);

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
       
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        return response;
    }
}