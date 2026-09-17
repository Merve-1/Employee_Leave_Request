using System.ComponentModel.DataAnnotations;

namespace LeaveRequests.Client.Models;

public class CreateLeaveRequestDto
{
    [Required(ErrorMessage = "Select an employee")]
    public int? EmployeeId { get; set; }
    [Required(ErrorMessage = "Select a Start Date")]
    public DateTime? StartDate { get; set; }
    [Required(ErrorMessage = "Select a End Date")]
    public DateTime? EndDate { get; set; }
    [Required(ErrorMessage = "Select a Leave Type")]
    public string LeaveType { get; set; } = string.Empty;
}