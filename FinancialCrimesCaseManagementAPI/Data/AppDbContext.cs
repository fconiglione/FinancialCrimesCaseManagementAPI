using FinancialCrimesCaseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialCrimesCaseManagementAPI.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<FraudCase> FraudCases { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
}