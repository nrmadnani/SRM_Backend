
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRMWebApiApp.Data;
using SRMWebApiApp.Services;

namespace SRMWebApiApp.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BondController: ControllerBase {
        private readonly IBondService _bondService;

        public BondController(IBondService bondService) {
            _bondService = bondService;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetBondData() {
            try{
                var result = await _bondService.GetBondData();
                return Ok(result);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}