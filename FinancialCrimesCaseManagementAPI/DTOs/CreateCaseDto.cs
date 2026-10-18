using System.ComponentModel.DataAnnotations;
using FinancialCrimesCaseManagementAPI.Models;

namespace FinancialCrimesCaseManagementAPI.DTOs;

public class CreateCaseDto
{
    [Required]
    public int TransactionId { get; set; }
    [MaxLength(100)]
    public string AdjudicatorNotes { get; set; } = null!;
    public string Status { get; set; } = null!;
}