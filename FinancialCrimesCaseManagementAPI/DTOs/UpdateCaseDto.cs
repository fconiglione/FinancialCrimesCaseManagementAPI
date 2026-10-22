using System.ComponentModel.DataAnnotations;

namespace FinancialCrimesCaseManagementAPI.DTOs;

public class UpdateCaseDto
{
    [MaxLength(100)]
    public string AdjudicatorNotes { get; set; } = null!;
    [Required]
    [RegularExpression("^(Open|Closed|Escalated)$", ErrorMessage = "Status must be exactly Open, Closed, or Escalated.")]
    public string Status { get; set; } = null!;
}