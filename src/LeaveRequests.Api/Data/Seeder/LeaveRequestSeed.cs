using LeaveRequests.Api.Entities;
using LeaveRequests.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequests.Api.Data.Seeder;

public class LeaveRequestSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaveRequest>().HasData(
            new LeaveRequest
            {
                Id = 1,
                EmployeeId = 1,
                StartDate = new DateTime(2026, 9, 15),
                EndDate = new DateTime(2026, 9, 18),
                Type = LeaveType.Sick,
                Status = LeaveStatus.Approved,
                CreatedAt = new DateTime(2026, 9, 7),
                ReviewerNote = "Tasks Assigned to Employee E109 Zeyad Youssef"
            },
            new LeaveRequest
            {
                Id = 2,
                EmployeeId = 2,
                StartDate = new DateTime(2026, 9, 16),
                EndDate = new DateTime(2026, 9, 17),
                Type = LeaveType.Vacation,
                Status = LeaveStatus.Approved,
                CreatedAt = new DateTime(2026, 9, 7),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 3,
                EmployeeId = 3,
                StartDate = new DateTime(2026, 9, 16),
                EndDate = new DateTime(2026, 10, 17),
                Type = LeaveType.Vacation,
                Status = LeaveStatus.Rejected,
                CreatedAt = new DateTime(2026, 9, 6),
                ReviewerNote = "Vacation max number of consecutive days is 7 days"
            },
            new LeaveRequest
            {
                Id = 4,
                EmployeeId = 4,
                StartDate = new DateTime(2026, 10, 16),
                EndDate = new DateTime(2026, 10, 17),
                Type = LeaveType.Unpaid,
                Status = LeaveStatus.Pending,
                CreatedAt = new DateTime(2026, 9, 16),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 5,
                EmployeeId = 5,
                StartDate = new DateTime(2026, 12, 16),
                EndDate = new DateTime(2026, 12, 17),
                Type = LeaveType.Vacation,
                Status = LeaveStatus.Approved,
                CreatedAt = new DateTime(2026, 9, 5),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 6,
                EmployeeId = 6,
                StartDate = new DateTime(2026, 9, 16),
                EndDate = new DateTime(2026, 10, 1),
                Type = LeaveType.Unpaid,
                Status = LeaveStatus.Pending,
                CreatedAt = new DateTime(2026, 9, 16),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 7,
                EmployeeId = 7,
                StartDate = new DateTime(2026, 11, 16),
                EndDate = new DateTime(2026, 11, 17),
                Type = LeaveType.Sick,
                Status = LeaveStatus.Pending,
                CreatedAt = new DateTime(2026, 9, 16),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 8,
                EmployeeId = 8,
                StartDate = new DateTime(2026, 10, 16),
                EndDate = new DateTime(2026, 10, 17),
                Type = LeaveType.Vacation,
                Status = LeaveStatus.Pending,
                CreatedAt = new DateTime(2026, 9, 16),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 9,
                EmployeeId = 9,
                StartDate = new DateTime(2026, 9, 10),
                EndDate = new DateTime(2026, 9, 17),
                Type = LeaveType.Unpaid,
                Status = LeaveStatus.Approved,
                CreatedAt = new DateTime(2026, 9, 1),
                ReviewerNote = null
            },
            new LeaveRequest
            {
                Id = 10,
                EmployeeId = 10,
                StartDate = new DateTime(2026, 9, 20),
                EndDate = new DateTime(2026, 10, 1),
                Type = LeaveType.Unpaid,
                Status = LeaveStatus.Approved,
                CreatedAt = new DateTime(2026, 9, 1),
                ReviewerNote = null
            }
            );
    }
}