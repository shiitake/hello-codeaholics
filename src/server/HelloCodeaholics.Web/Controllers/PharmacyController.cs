using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelloCodeaholics.Services.Application;
using HelloCodeaholics.Core.Interfaces;
using HelloCodeaholics.Core.Domain.Entities;

namespace HelloCodeaholics.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private IPharmacyService _pharmacyService { get;set; }
        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pharmacy>> GetPharmacy(int id)
        {
            var response = await _pharmacyService.GetPharmacyById(id);
            if (response is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Pharmacy>>> GetPharmacies()
        {
            var response = await _pharmacyService.GetPharmacyList();
            if (response is null || response.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePharmacy(int id,  Pharmacy pharmacy)
        {
            if (id != pharmacy.Id)
            {
                return BadRequest();
            }

            if (!await _pharmacyService.PharmacyExists(id))
            {
                return NotFound();
            }

            await _pharmacyService.UpdatePharmacy(pharmacy);
                
            // we could also return updated value if client requests it.
            return NoContent();
        }
    }
}
