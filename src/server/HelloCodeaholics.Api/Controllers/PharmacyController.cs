using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelloCodeaholics.Services.Application;
using HelloCodeaholics.Core.Interfaces;
using HelloCodeaholics.Core.Domain.Entities;

namespace HelloCodeaholics.Web.Controllers;

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
    public async Task<ActionResult<Pharmacy>> GetPharmacy(int id)
    {
        var response = await _pharmacyService.GetPharmacyById(id);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<Pharmacy>>> GetPharmacyList(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _pharmacyService.GetPharmacyList(pageNumber, pageSize);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePharmacy(Pharmacy pharmacy)
    {
        var response = await _pharmacyService.UpdatePharmacy(pharmacy);
        return response != null 
            ? Ok(response)
            : Ok(new { Message = "Pharmacy does not exist." });
    }
}
