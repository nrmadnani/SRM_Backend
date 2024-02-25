
using Microsoft.AspNetCore.Mvc;
using SRMWebApiApp.Dtos;
using SRMWebApiApp.Models;
using SRMWebApiApp.Services;

namespace SRMWebApiApp.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BondController: ControllerBase {
        private readonly IBondService _bondService;

        public BondController(IBondService bondService) {
            _bondService = bondService;
        }
        
        [HttpGet("/all")]
        public async Task<ActionResult> GetBondData() {
            try{
                var result = await _bondService.GetBondData();
                return Ok(result);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBond(int id){
            try{
                var result = await _bondService.GetBond(id);
                return Ok(result);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBondById(int id){
            try{
                var deletedStatus = await _bondService.DeleteBondById(id);
                if(deletedStatus)
                    return Ok("Data deleted");
                else 
                    return BadRequest("Data not deleted");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            
        } 

        [HttpDelete("entity/{id}")]
        public async Task<ActionResult> DeleteBond(int id){
            try{
                var deletedEntity = await _bondService.DeleteBond(id);
                if (deletedEntity != null){
                    return Ok(deletedEntity);
                }
                else {
                    return BadRequest("Data does not exist");
                }
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}