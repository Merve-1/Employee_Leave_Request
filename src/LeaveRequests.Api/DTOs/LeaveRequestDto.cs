using LeaveRequests.Api.Enums;

namespace LeaveRequests.Api.DTOs;

public class LeaveRequestDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate {get; set;}
    public LeaveType Type {get; set;}
    public LeaveStatus Status {get; set;}
    public DateTime CreatedAt {get; set;}
    public string? ReviewerNote {get; set;}
}

