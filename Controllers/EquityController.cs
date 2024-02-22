using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRMWebApiApp.Data;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class EquityController : ControllerBase{
        private readonly IVP_3308_v3Context _context;

        public EquityController(IVP_3308_v3Context context){
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SecuritySummary>>> GetAllSecurities(){
            var securities = await _context.SecuritySummaries.ToListAsync();
            return Ok(securities);
        }

    }
}