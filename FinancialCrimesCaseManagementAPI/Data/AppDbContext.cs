using FinancialCrimesCaseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialCrimesCaseManagementAPI.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<FraudCase> FraudCases { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FraudCase>()
            .HasOne(fc => fc.Transaction)
            .WithOne()
            .HasForeignKey<FraudCase>(fc => fc.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FraudCase>()
            .Property(fc => fc.TransactionId)
            .IsRequired(false);
    }
}