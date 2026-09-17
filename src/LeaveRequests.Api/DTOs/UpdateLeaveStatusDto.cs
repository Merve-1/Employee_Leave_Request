using System.ComponentModel.DataAnnotations;
using LeaveRequests.Api.Enums;

namespace LeaveRequests.Api.DTOs;

public class UpdateLeaveStatusDto
{
    [EnumDataType(typeof(LeaveStatus))]
    public LeaveStatus Status { get; set; }
    
    [MaxLength(300)]
    public string? ReviewerNote { get; set; }
}