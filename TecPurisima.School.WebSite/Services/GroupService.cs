using Newtonsoft.Json;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Services;

public class GroupService: IGroupService
{
    private readonly string _baseUrl = "http://localhost:5279/";
    private readonly string _endpoint = "api/Groups";
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public GroupService(IHttpContextAccessor httpContextAccessor)
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
    
    public async Task<Response<List<SchoolGroupDto>>> GetAllAsync()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = CreateHttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<SchoolGroupDto>>>(json);
        
        return response;
    }

    public async Task<Response<SchoolGroupDto>> GetByIdAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client = CreateHttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<SchoolGroupDto>>(json);
        
        return response;
    }

    public async Task<Response<SchoolGroupDto>> SaveAsync(SchoolGroupDto group)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(group);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = CreateHttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<SchoolGroupDto>>(json);
        
        return response;
    }

    public async Task<Response<SchoolGroupDto>> UpdateAsync(SchoolGroupDto group)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(group);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = CreateHttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<SchoolGroupDto>>(json);

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