using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SRMWebApiApp.Dtos;
using SRMWebApiApp.Services;

namespace SRMWebApiApp.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class EquityController : ControllerBase{

        private readonly IEquityService _equityService;
        public EquityController(IEquityService equityService)
        {
            _equityService = equityService;
        }


        [HttpGet]
        public async Task<ActionResult> GetEquityData(){
            try{
                var result = await _equityService.GetEquityData();
                return Ok(result);
            } catch(Exception e){
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut]
        public async Task<ActionResult> PutEquityData([FromBody] UpdateEquityDTO dto){
            try{
                var result = await _equityService.UpdateEquityData(dto);
                return Ok("Hello");
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}