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
        var result = await _caseService.CreateCase(request);

        return Created($"/api/v1/cases/{result.Id}", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCase([FromRoute] int id, UpdateCaseDto request)
    {
        var result = await _caseService.UpdateCase(id, request);

        return Ok(result);
    }
}