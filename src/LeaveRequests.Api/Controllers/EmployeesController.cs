using LeaveRequests.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveRequests.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController: ControllerBase
{
    private readonly IEmployeeService _employeeService;
    
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    /// <summary>
    /// Return all of the dummy json users as a list and searching by user name is allowed 
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search)
    {
        var employees = await _employeeService.GetAllAsync(search);
        
        return Ok(employees);
    }
}