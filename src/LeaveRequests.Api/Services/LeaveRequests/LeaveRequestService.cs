using LeaveRequests.Api.Data;
using LeaveRequests.Api.DTOs;
using LeaveRequests.Api.Entities;
using LeaveRequests.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequests.Api.Services.LeaveRequests;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly LeaveDbContext _context;
    private readonly IEmployeeService _employeeService;

    public LeaveRequestService(LeaveDbContext context, IEmployeeService employeeService)
    {
        _context = context;
        _employeeService = employeeService;
    }
    
    public async Task<IReadOnlyList<LeaveRequestDto>> GetAllAsync(LeaveStatus? status, int? employeeId, int page, int pageSize)
    {
        var query = _context.LeaveRequests.AsNoTracking().AsQueryable();
        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
        }

        var leaveRequests = await query.OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var employees = await _employeeService.GetAllAsync();
        
        var employeeLookup = employees.ToDictionary(e => e.Id);
        return leaveRequests.Select(leaveRequest =>
        {
            employeeLookup.TryGetValue(leaveRequest.EmployeeId, out var employee);
            return new LeaveRequestDto
            {
                Id = leaveRequest.Id,
                EmployeeId = leaveRequest.EmployeeId,
                EmployeeName = employee?.Name ?? "Unknown",
                Department = employee?.Department ?? "Unknown",
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Type = leaveRequest.Type,
                Status = leaveRequest.Status,
                CreatedAt = leaveRequest.CreatedAt,
                ReviewerNote = leaveRequest.ReviewerNote
            };
        }).ToList();

    }
}