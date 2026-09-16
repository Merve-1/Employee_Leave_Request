using LeaveRequests.Api.Data.Seeder;
using LeaveRequests.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequests.Api.Data;

public class LeaveDbContext : DbContext
{
    public LeaveDbContext(DbContextOptions<LeaveDbContext> options) : base(options)
    {
        
    }

    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.EmployeeId)
                .IsRequired();

            entity.Property(l => l.StartDate)
                .IsRequired();

            entity.Property(l => l.EndDate)
                .IsRequired();
            
            entity.Property(l => l.Type)
                .HasConversion<string>()
                .IsRequired();
            
            entity.Property(l => l.Status)
                .HasConversion<string>()
                .IsRequired();
            
            entity.Property(l => l.CreatedAt)
                .IsRequired();
            
            entity.Property(l => l.ReviewerNote)
                .HasMaxLength(500)
                .IsRequired(false);
            entity.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "CK_LeaveRequests_DateRange",
                    "[EndDate] >= [StartDate]");
            
                table.HasCheckConstraint(
                    "Ck_LeaveRequests_Status", 
                    "[Status] IN ('Pending', 'Approved', 'Rejected')");
            });
            

        });
    LeaveRequestSeed.Seed(modelBuilder);
    }

}