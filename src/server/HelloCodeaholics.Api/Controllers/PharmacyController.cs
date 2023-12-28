using Microsoft.AspNetCore.Mvc;
using HelloCodeaholics.Services.Interfaces;
using HelloCodeaholics.Common.Models;

namespace HelloCodeaholics.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PharmacyController : ControllerBase
{
    private readonly IPharmacyService _pharmacyService;
    public PharmacyController(IPharmacyService pharmacyService)
    {
        _pharmacyService = pharmacyService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PharmacyViewModel>> GetPharmacy(int id)
    {
        var response = await _pharmacyService.GetPharmacyById(id);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<PharmacyViewModel>>> GetPharmacyList(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _pharmacyService.GetPharmacyList(pageNumber, pageSize);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePharmacy(PharmacyViewModel pharmacy)
    {
        var response = await _pharmacyService.UpdatePharmacy(pharmacy);
        return response != null 
            ? Ok(response)
            : Ok(new { Message = "Pharmacy does not exist." });
    }
}
