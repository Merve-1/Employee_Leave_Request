using System.ComponentModel.DataAnnotations;
using LeaveRequests.Api.Enums;

namespace LeaveRequests.Api.DTOs;

public class CreateLeaveRequestDto
{
    [Range(1, int.MaxValue)]
    public int EmployeeId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    [EnumDataType(typeof(LeaveType))]
    public LeaveType Type { get; set; }
    
    [MaxLength(300)]
    public string? ReviewerNote {get; set;}
}