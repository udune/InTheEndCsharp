using System.Text;
using System.Text.Json;
using APIApp.Models;

namespace APIApp.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(string? baseUrl)
    {
        _httpClient = new HttpClient();
        if (!string.IsNullOrEmpty(baseUrl))
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
        }
    }

    public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return await HandleResponseAsync<TResponse>(response);
    }
    
    public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string url, 
        TRequest requestBody,
        Dictionary<string, string>? headers = null)
    {
        string json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
        var response = await _httpClient.SendAsync(request);
        // var response = await _httpClient.PostAsync(url, content);
        return await HandleResponseAsync<TResponse>(response);
    } 
    
    private async Task<ApiResponse<TResponse>> HandleResponseAsync<TResponse>(HttpResponseMessage response)
    {
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            TResponse? data = JsonSerializer.Deserialize<TResponse>(responseBody, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return new ApiResponse<TResponse>((int)response.StatusCode, data!);
        }
        else
        {
            return new ApiResponse<TResponse>((int)response.StatusCode, errorMessage: responseBody);
        }
    }
}