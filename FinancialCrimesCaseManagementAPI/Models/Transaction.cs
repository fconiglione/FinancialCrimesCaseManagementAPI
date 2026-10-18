namespace FinancialCrimesCaseManagementAPI.Models;

public class Transaction
{
    public int Id { get; set; }
    public double Amount { get; set; }
    public string Merchant { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public bool IsSuspicious { get; set; }
}