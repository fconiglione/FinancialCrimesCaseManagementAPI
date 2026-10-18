namespace FinancialCrimesCaseManagementAPI.Models;

public class FraudCase
{
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public string AdjudicatorNotes { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Transaction Transaction { get; set; } = null!;
}