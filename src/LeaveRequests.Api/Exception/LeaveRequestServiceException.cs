namespace LeaveRequests.Api.Services;

public class LeaveRequestServiceException: Exception
{
    public LeaveRequestServiceException(string message) : base(message)
    {
        
    }

    public LeaveRequestServiceException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}