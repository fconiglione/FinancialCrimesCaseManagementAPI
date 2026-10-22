using System.Reflection.Metadata.Ecma335;
using FinancialCrimesCaseManagementAPI.Data;
using FinancialCrimesCaseManagementAPI.DTOs;
using FinancialCrimesCaseManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace FinancialCrimesCaseManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CasesController : ControllerBase
{
    private readonly ICaseService _caseService;

    public CasesController(ICaseService caseService)
    {
        _caseService = caseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCase(CreateCaseDto request)
    {
        if (request.TransactionId is null)
        {
            return BadRequest("TransactionId is required.");
        }

        if (request.AdjudicatorNotes.Length > 100)
        {
            return BadRequest("AdjudicatorNotes must be 100 characters or less.");
        }

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            request.Status = "Open";
        }  else if (request.Status.Trim().ToLower() != "Open" || request.Status.Trim().ToLower() != "Closed" || request.Status.Trim().ToLower() != "Escalated")
        {
            return BadRequest("Status must be one of 'Open', 'Closed', or 'Escalated'.");
        }
        
        var result = await _caseService.CreateCase(request);

        if (result == null)
        {
            return BadRequest();
        }

        return Created($"/api/v1/cases/{result.Id}", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCase([FromRoute] int? id, UpdateCaseDto request)
    {
        if (id is null)
        {
            return BadRequest("FraudCaseId is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            request.Status = "Open";
        }  else if (request.Status.Trim().ToLower() != "Open" || request.Status.Trim().ToLower() != "Closed" || request.Status.Trim().ToLower() != "Escalated")
        {
            return BadRequest("Status must be one of 'Open', 'Closed', or 'Escalated'.");
        }
        
        var result = await _caseService.UpdateCase(id, request);
        
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("search")]
    public ActionResult SearchCasesByMerchant(string merchant)
    {
        if (String.IsNullOrWhiteSpace(merchant))
        {
            return BadRequest("Merchant must not be null.");
        }
        
        var result = _caseService.SearchCasesByMerchant(merchant);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedCases([FromQuery] string status, [FromQuery] int page, [FromQuery] int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than 0.");
        }

        var result = await _caseService.GetPagedCases(status, page, pageSize);

        return Ok(result);
    }

    // Endpoint to explicitly escalate a case
    [HttpPut("{id}/escalate")]
    public async Task<IActionResult> EscalateCase([FromRoute] int? id)
    {
        if (id is null)
        {
            return BadRequest("FraudCaseId is required.");
        }
        
        var result = await _caseService.EscalateCase(id);

        return Ok(result);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterCases([FromQuery] string? status, 
        [FromQuery] double? minAmount, [FromQuery] string? sortBy, 
        [FromQuery] bool? sortDescending)
    {
        var result = await _caseService.FilterCases(status, minAmount, sortBy, sortDescending);

        if (result == null) return BadRequest();

        return Ok(result);
    }
}