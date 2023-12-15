using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelloCodeaholics.Services.Application;
using HelloCodeaholics.Core.Interfaces;

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

        [HttpGet]
        public async Task<ActionResult> GetPharmacies()
        {
            var response = await _pharmacyService.GetPharmacyList();
            if (response is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }



    }
}
