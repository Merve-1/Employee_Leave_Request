using LeaveRequests.Api.DTOs;
using LeaveRequests.Api.Enums;

namespace LeaveRequests.Api.Services.LeaveRequests;

public interface ILeaveRequestService
{
    Task<IReadOnlyList<LeaveRequestDto>> GetAllAsync(LeaveStatus? status, int? employeeId, 
        int page, int pageSize);
    
    Task<LeaveRequestDto?> GetByIdAsync(int id);
    
    Task<LeaveRequestDto> CreateAsync(CreateLeaveRequestDto request);
}