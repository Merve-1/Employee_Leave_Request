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
    public async Task<IReadOnlyList<LeaveRequestDto>> GetAllAsync(LeaveStatus? status, int? employeeId, int page,
        int pageSize)
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
            return MapToDto(leaveRequest, employee);
        }).ToList();
    }

    public async Task<LeaveRequestDto?> GetByIdAsync(int id)
    {
        var leaveRequest = await _context.LeaveRequests.AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id);
        if (leaveRequest is null)
        {
            return null;
        }
        var employee = await _employeeService.GetByIdAsync(leaveRequest.EmployeeId);

        return MapToDto(leaveRequest, employee);
    }

    public async Task<LeaveRequestDto> CreateAsync(CreateLeaveRequestDto request)
    {
        var employee = await _employeeService.GetByIdAsync(request.EmployeeId);
        if (request.EndDate < request.StartDate)
        {
            throw new ArgumentException("EndDate must be before StartDate");
        }
        if (employee is null)
        {
            throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found.");
        }

        var leaveRequest = new LeaveRequest
        {
            EmployeeId = request.EmployeeId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Type = request.Type,
            Status = LeaveStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            ReviewerNote = request.ReviewerNote
        };
        _context.LeaveRequests.Add(leaveRequest);
        await _context.SaveChangesAsync();
        

        return MapToDto(leaveRequest, employee);
    }

    public async Task<LeaveRequestDto?> UpdateStatusAsync(int id, UpdateLeaveStatusDto request)
    {
        var leaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(l => l.Id == id);
        if (leaveRequest is null)
        {
            return null;
        }

        if (leaveRequest.Status != LeaveStatus.Pending)
        {
            throw new InvalidOperationException("Only pending leave requests can be approved or rejected");
        }

        if (request.Status != LeaveStatus.Approved && request.Status != LeaveStatus.Rejected)
        {
            throw new InvalidOperationException("Status must be approved or rejected");
        }
        
        leaveRequest.Status = request.Status;
        leaveRequest.ReviewerNote =  request.ReviewerNote;
        
        await _context.SaveChangesAsync();
        var employee = await _employeeService.GetByIdAsync(leaveRequest.EmployeeId);
        
        return MapToDto(leaveRequest, employee);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var leaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(l => l.Id == id);

        if (leaveRequest is null)
        {
            return false;
        }

        if (leaveRequest.Status != LeaveStatus.Pending)
        {
            throw new InvalidOperationException("Only pending leave requests can be deleted");
        }
        _context.LeaveRequests.Remove(leaveRequest);
        await _context.SaveChangesAsync();

        return true;
    }

    private LeaveRequestDto MapToDto(LeaveRequest leaveRequest, EmployeeDto? employee)
    {
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
    }
    
    
}