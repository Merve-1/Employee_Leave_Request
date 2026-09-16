namespace LeaveRequests.Api.DTOs;

public class DummyJsonUser
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DummyJsonCompany? Company { get; set; }
}