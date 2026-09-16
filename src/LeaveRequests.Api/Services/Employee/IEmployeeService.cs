using LeaveRequests.Api.DTOs;

namespace LeaveRequests.Api.Services;

public interface IEmployeeService
{
    Task<EmployeeDto?> GetByIdAsync(int id);
    //last endpoint filterable by user
    Task<IReadOnlyList<EmployeeDto>> GetAllAsync(string? search = null);
}