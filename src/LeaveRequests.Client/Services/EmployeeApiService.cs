using LeaveRequests.Client.Models;

namespace LeaveRequests.Client.Services;

public class EmployeeApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public EmployeeApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<EmployeeDto>> GetEmployeesAsync()
    {
        var client = _httpClientFactory.CreateClient("LeaveApi");
        return await client.GetFromJsonAsync<List<EmployeeDto>>("api/employees")?? [];
    }
}