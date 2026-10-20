using System.ComponentModel.DataAnnotations;

namespace FinancialCrimesCaseManagementAPI.DTOs;

public class UpdateCaseDto
{
    [MaxLength(100)]
    public string AdjudicatorNotes { get; set; } = null!;
    public string Status { get; set; } = null!;
}