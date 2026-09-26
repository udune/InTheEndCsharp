using System.Text.Json;
using APIApp.Dtos;
using APIApp.Services;

namespace APIApp;

public partial class Form1 : Form
{
    const string BaseUrl = "https://localhost:7038";
    private ApiClient _apiClient = new ApiClient(BaseUrl);
    readonly HttpClient httpClient = new HttpClient();
    
    public Form1()
    {
        InitializeComponent();
    }

    private async void btnGetUser_Click(object sender, EventArgs e)
    {
        // List<UserDto>? userDtos = await GetUsersAsync();
        // dgv.DataSource = userDtos;
        
        // UserDto? user = await GetUserAsync(2);
        // if (user != null)
        // {
        //     dgv.DataSource = new List<UserDto> { user };
        // }
        
        // UserDto? user = await GetUserAsync(3);
        // if (user != null)
        // {
        //     dgv.DataSource = new List<UserDto> { user };
        // }
        
        var response = await _apiClient.GetAsync<UserDto>("/Users/3");
        if (response.IsSuccess)
        {
            dgv.DataSource = new List<UserDto>() { response.Data };
        }
        else
        {
            MessageBox.Show($"Error: {response.StatusCode} - {response.ErrorMessage}");
        }
    }
    
    private async void btnGetUsers_Click(object sender, EventArgs e)
    {
        var response = await _apiClient.GetAsync<List<UserDto>>("/Users");
        if (response.IsSuccess)
        {
            dgv.DataSource = response.Data;
        }
        else
        {
            MessageBox.Show($"Error: {response.StatusCode} - {response.ErrorMessage}");
        }
    }
    
    private async void btnPostUser_Click(object sender, EventArgs e)
    {
        var newUser = new UserDto()
        {
            Name = "New User",
            Email = "test@test.com",
            Username = "newuser",
        };
        
        var response = await _apiClient.PostAsync<UserDto, UserDto>(
            "/Users", 
            newUser, 
            headers: new Dictionary<string, string>() { { "X-API-KEY", "api-value" } });
        if (response.IsSuccess)
        {
            dgv.DataSource = new List<UserDto>() { response.Data };
        }
        else
        {
            MessageBox.Show($"Error: {response.StatusCode} - {response.ErrorMessage}");
        }
    }

    private async Task<List<UserDto>> GetUsersAsync()
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/Users");
        var jsonStr = await response!.Content.ReadAsStringAsync();
        
        List<UserDto>? userDtos = JsonSerializer.Deserialize<List<UserDto>>(jsonStr, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        return userDtos ?? new List<UserDto>();
    }
    
    private async Task<UserDto?> GetUserAsync(int userId)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/Users/{userId}");
        var jsonStr = await response!.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            UserDto? user = JsonSerializer.Deserialize<UserDto>(jsonStr, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });    
            return user;
        }
        else
        {
            MessageBox.Show($"Error: {response.StatusCode} - {jsonStr}");
            return null;
        }
    }
}