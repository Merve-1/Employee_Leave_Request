namespace LeaveRequests.Api.Services;

public class EmployeeServiceException : Exception
{
    public EmployeeServiceException(string message) : base(message)
    {
        
    }

    public EmployeeServiceException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}