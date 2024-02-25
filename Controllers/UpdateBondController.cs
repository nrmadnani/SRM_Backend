using Microsoft.AspNetCore.Mvc;
using SRMWebApiApp.Dtos;
using SRMWebApiApp.Services;

namespace SRMWebApiApp.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class UpdateBondData: ControllerBase {
        private readonly IUpdateBondService _bondService;

        public UpdateBondData(IUpdateBondService bondService) {
            _bondService = bondService;
        }

    [HttpPut]
        public async Task<ActionResult> UpdateBond([FromBody] UpdateBondDTO dto) {
            try{
                var result = await _bondService.UpdateBondData(dto);
                return Ok(result);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}