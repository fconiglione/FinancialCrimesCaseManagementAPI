using FinancialCrimesCaseManagementAPI.DTOs;

namespace FinancialCrimesCaseManagementAPI.Services;

public interface ICaseService
{
    public Task<CaseSummaryDto> CreateCase(CreateCaseDto request);
    public Task<CaseSummaryDto> UpdateCase(int? id, UpdateCaseDto request);
    public Task<List<CaseSummaryDto>> SearchCasesByMerchant(string merchant);
    public Task<PagedResultDto<CaseSummaryDto>> GetPagedCases(string status, int page, int pageSize);
}