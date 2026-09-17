using LeaveRequests.Api.Data;
using LeaveRequests.Api.DTOs;
using LeaveRequests.Api.Entities;
using LeaveRequests.Api.Enums;
using LeaveRequests.Api.Services;
using LeaveRequests.Api.Services.LeaveRequests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LeaveRequests.Api.Tests;

public class LeaveRequestServiceTests
{
    private static LeaveDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<LeaveDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        return new LeaveDbContext(options);
    }


    private static async Task AddLeaveRequestAsync(LeaveDbContext context, int id, LeaveStatus status)
    {
        context.LeaveRequests.Add(new LeaveRequest
        {
            Id = id,
            EmployeeId = 1,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 1, 10),
            Type = LeaveType.Vacation,
            Status = status,
            CreatedAt = DateTime.UtcNow
        });
        await context.SaveChangesAsync();
    }

    private static Mock<IEmployeeService> CreateEmployeeServiceMock()
    {
        var mock = new Mock<IEmployeeService>();
        mock.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new EmployeeDto{
            Id = 1, Name = "Marwa Dev", Department= "HR"});
        return mock;
    }

    [Fact]
    public async Task UpdateStatus_PendingToApproved_UpdatesStatus()
    {
        await using var context  =  CreateDbContext();
        await AddLeaveRequestAsync(context, 1, LeaveStatus.Pending);

        var employeeService = CreateEmployeeServiceMock();
        
        var service =new LeaveRequestService(context, employeeService.Object);

        var request = new UpdateLeaveStatusDto
        {
            Status = LeaveStatus.Approved,
            ReviewerNote = "Approved by HR"
        };
        var result = await service.UpdateStatusAsync(1, request);
        Assert.NotNull(result);
        Assert.Equal(LeaveStatus.Approved, result.Status);
        Assert.Equal("Approved by HR", result.ReviewerNote);
    }
}