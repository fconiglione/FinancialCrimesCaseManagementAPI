using FinancialCrimesCaseManagementAPI.Data;
using FinancialCrimesCaseManagementAPI.DTOs;
using FinancialCrimesCaseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialCrimesCaseManagementAPI.Services;

public class CaseService: ICaseService
{
    private readonly AppDbContext _context;

    public CaseService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<CaseSummaryDto> CreateCase(CreateCaseDto request)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(x => x.Id == request.TransactionId);
        
        if (transaction == null) return null!;
        
        var newCase = new FraudCase(
            request.TransactionId,
            request.AdjudicatorNotes,
            request.Status,
            transaction
            );
        
        _context.FraudCases.Add(newCase);
        await _context.SaveChangesAsync();

        return new CaseSummaryDto
        {
            Id = newCase.Id,
            AdjudicatorNotes = newCase.AdjudicatorNotes,
            Status = newCase.Status,
            Transaction = newCase.Transaction
        };
    }

    public async Task<CaseSummaryDto> UpdateCase(int? id, UpdateCaseDto request)
    {
        var updatedCase = await _context.FraudCases.Include(x => x.Transaction).FirstOrDefaultAsync(x => x.Id == id);

        if (updatedCase == null) return null!;
        
        updatedCase.AdjudicatorNotes = request.AdjudicatorNotes;
        updatedCase.Status = request.Status;

        await _context.SaveChangesAsync();

        return new CaseSummaryDto
        {
            Id = updatedCase.Id,
            AdjudicatorNotes = updatedCase.AdjudicatorNotes,
            Status = updatedCase.Status,
            Transaction = updatedCase.Transaction
        };
    }

    public async Task<List<CaseSummaryDto>> SearchCasesByMerchant(string merchant)
    {
        var filteredCases = await _context.FraudCases
            .Include(c => c.Transaction)
            .Where(c => c.Transaction.Merchant.Contains(merchant))
            .Select(c => new CaseSummaryDto
            {
                Id = c.Id,
                AdjudicatorNotes = c.AdjudicatorNotes,
                Status = c.Status,
                Transaction = c.Transaction
            })
            .ToListAsync(); 

        if (!filteredCases.Any())
        {
            return null;
        }

        return filteredCases;
    }

    public async Task<PagedResultDto<CaseSummaryDto>> GetPagedCases(string status, int page, int pageSize)
    {
        var baseQuery = _context.FraudCases.Where(c => c.Status == status);

        var totalCount = await baseQuery.CountAsync();

        var pagedCases = await baseQuery
            .Include(c => c.Transaction)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CaseSummaryDto 
            {
                Id = c.Id,
                AdjudicatorNotes = c.AdjudicatorNotes,
                Status = c.Status,
                Transaction = c.Transaction
            })
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResultDto<CaseSummaryDto>
        {
            Items = pagedCases,
            TotalCount = totalCount,
            CurrentPage = page,
            TotalPages = totalPages
        };
    }

    public async Task<CaseSummaryDto> EscalateCase(int? id)
    {
        var identifiedCase = await _context.FraudCases.Include(c=>c.Transaction).FirstOrDefaultAsync(c => c.Id == id);

        if (identifiedCase is null) return null;

        identifiedCase.Status = "Escalated";
        identifiedCase.AdjudicatorNotes = "Case has been escalated.";

        await _context.SaveChangesAsync();

        return new CaseSummaryDto()
        {
            Id = identifiedCase.Id,
            AdjudicatorNotes = identifiedCase.AdjudicatorNotes,
            Status = identifiedCase.Status,
            Transaction = identifiedCase.Transaction
        };
    }
}