using FinancialCrimesCaseManagementAPI.DTOs;

namespace FinancialCrimesCaseManagementAPI.Services;

public interface ICaseService
{
    public Task<CaseSummaryDto> CreateCase(CreateCaseDto request);
    public Task<CaseSummaryDto> UpdateCase(UpdateCaseDto request);
}