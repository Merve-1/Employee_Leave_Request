using LeaveRequests.Api.DTOs;
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

    /// <summary>
    ///Return all leave requests and filtering by status/ employeeId / page / pageSize allowed 
    /// </summary>
    /// <param name="status"></param>
    /// <param name="employeeId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] LeaveStatus? status, [FromQuery] int? employeeId,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            return BadRequest(
                new
                {
                    error = "Page must be greater than zero"
                });
        }
        if (employeeId > 30)
        {
            return BadRequest(
                new
                {
                    error = "employee id must be less than 31"
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

    /// <summary>
    ///  Return leave request by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leaveRequest = await _leaveRequestService.GetByIdAsync(id);
        if (leaveRequest is null)
        {
            return NotFound(new
            {
                error= $"leave request with id {id} was not found"
            });
        }
        return Ok(leaveRequest);
    }
    
    /// <summary>
    /// Create new Leave Request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveRequestDto request)
    {
        var leaveRequest = await _leaveRequestService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = leaveRequest.Id }, leaveRequest);
    }

    /// <summary>
    /// Update the pending status to approved or rejected
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateLeaveStatusDto request)
    {
        var leaveRequest = await _leaveRequestService.UpdateStatusAsync(id, request);
        if (leaveRequest is null)
        {
            return NotFound(new
            {
                error = $"Leave Request with id {id} was not found"
            });
        }
        return Ok(leaveRequest);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _leaveRequestService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new
            {
                error = $"Leave request with id {id} was nto found"
            });
        }

        return NoContent();
    }
    
}