using System.Text.Json.Serialization;

namespace LeaveRequests.Client.Models;

public class LeaveRequestDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [JsonPropertyName("type")]
    public string LeaveType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}