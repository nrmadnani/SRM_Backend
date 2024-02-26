
using Microsoft.AspNetCore.Mvc;
using SRMWebApiApp.Services;

namespace SRMWebApiApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase {
        private readonly IUtilityService _utilityService;
        public HomeController(IUtilityService utilityService){
            _utilityService = utilityService;
        }
        
        [HttpGet("/activeCount")]
        public async Task<ActionResult> GetActiveSecuritiesCount() {
            int result = await _utilityService.GetActiveSecuritiesCount();
            return Ok(result);
            
        }

        [HttpGet("/inactiveCount")]
        public async Task<ActionResult> GetInActiveSecuritiesCount() {
            int result = await _utilityService.GetInactiveSecuritiesCount();
            return Ok(result);
            
        }
    }
}