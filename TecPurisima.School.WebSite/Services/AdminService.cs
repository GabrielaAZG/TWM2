using Newtonsoft.Json;
using TecPurisima.School.Core.Dto;
using TecPurisima.School.Core.Http;
using TecPurisima.School.WebSite.Services.Interfaces;

namespace TecPurisima.School.WebSite.Services;

public class AdminService : IAdminService
{
    private readonly string _baseUrl = "http://localhost:5279/";
    private readonly string _endpoint = "api/Admins";

    public async Task<Response<List<AdminDto>>> GetAllAsync()
    {
        var url = $"{_baseUrl}{_endpoint}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<AdminDto>>>(json);
        
        return response;
    }

    public async Task<Response<AdminDto>> GetByIdAsync(int id)
    {
        var url = $"{_baseUrl}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<AdminDto>>(json);
        
        return response;
    }

    public async Task<Response<AdminDto>> SaveAsync(AdminDto admin)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(admin);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<AdminDto>>(json);
        
        return response;
    }

    public async Task<Response<AdminDto>> UpdateAsync(AdminDto admin)
    {
        var url = $"{_baseUrl}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(admin);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<AdminDto>>(json);

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