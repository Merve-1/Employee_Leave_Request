using System.Net;
using System.Text.Json;
using LeaveRequests.Api.DTOs;
using Microsoft.Extensions.Caching.Memory;
namespace LeaveRequests.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private const string EmployeesCacheKey = "Employees";

    public EmployeeService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }
    
    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"users/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            var employee = await response.Content.ReadFromJsonAsync<DummyJsonUser>();

            if (employee is null)
            {
                throw new EmployeeServiceException("Employee service returned invalid response");
            }

            return MapEmployee(employee);
        }
        catch (TaskCanceledException e )
        {
            throw new EmployeeServiceException("Employee service timed out", e);
        }
        catch (HttpRequestException e)
        {
            throw new EmployeeServiceException("Employee service is currently unavailable", e);
        }
        catch (JsonException e)
        {
            throw new EmployeeServiceException("Employee service returned invalid response", e);
        }
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync(string? search = null)
    {
        var employees = await GetEmployeesFromCacheAsync();
        if (string.IsNullOrWhiteSpace(search))
        {
            return employees;
        }
        return employees.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    private async Task<IReadOnlyList<EmployeeDto>> GetEmployeesFromCacheAsync()
    {
        var cachedEmployees = _cache.Get<IReadOnlyList<EmployeeDto>>(EmployeesCacheKey);
        if (cachedEmployees is not null)
        {
            return cachedEmployees;
        }

        try
        {
            var response = await _httpClient.GetAsync("users?limit=100");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<DummyJsonUserResponse>();

            if (result?.Users is null)
            {
                throw new EmployeeServiceException("Invalid Response");
            }
            var employees = result.Users.Select(MapEmployee).ToList();
            _cache.Set(EmployeesCacheKey, employees, TimeSpan.FromMinutes(10));
            return employees;
        }catch (TaskCanceledException e)
        {
            throw new EmployeeServiceException("Employee service timed out",e);
        }
        catch (HttpRequestException e)
        {
            throw new EmployeeServiceException("Employee service is currently unavailable", e);
        }
        catch (JsonException e)
        {
            throw new EmployeeServiceException("Employee service returned invalid response", e);
        }
    }

    public static EmployeeDto MapEmployee(DummyJsonUser user)
    {
        return new EmployeeDto()
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Department = user.Company?.Department ?? "Unknown"
        };
    }
}