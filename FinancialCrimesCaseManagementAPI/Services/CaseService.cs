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
}