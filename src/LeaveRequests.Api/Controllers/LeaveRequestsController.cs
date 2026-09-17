using LeaveRequests.Api.Enums;
using LeaveRequests.Api.Services.LeaveRequests;
using Microsoft.AspNetCore.Mvc;

namespace LeaveRequests.Api.Controllers;

[ApiController]
[Route("api/leave-requests")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestsController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] LeaveStatus? status,
        [FromQuery] int? employeeId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            return BadRequest(
                new
                {
                    error = "Page must be greater than zero"
                });
            
        }
        if (employeeId > 10)
        {
            return BadRequest(
                new
                {
                    error = "employee id must be less than 11"
                });
            
        }
        if (pageSize < 1)
        {
            
            return BadRequest(
                new
                {
                    error = "PageSize must be greater than zero"
                });
        }

        if (pageSize > 100)
        {
            return BadRequest(
                new
                {
                    error = "PageSize cannot exceed 100"
                });
        }

        var leaveRequests = await _leaveRequestService.GetAllAsync(
            status, employeeId, page, pageSize);
        return Ok(leaveRequests);
    }
}