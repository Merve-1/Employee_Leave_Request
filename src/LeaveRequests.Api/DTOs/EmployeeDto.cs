using System.ComponentModel.DataAnnotations;

namespace LeaveRequests.Api.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Department { get; set; } = string.Empty;
}