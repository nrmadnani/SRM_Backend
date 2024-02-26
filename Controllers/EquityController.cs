using System.Collections;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEquity(int id){
            try{
                var result = await _equityService.GetEquity(id);
                return Ok(result);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquityById(int id){
            try{
                var deletedStatus = await _equityService.DeleteEquityById(id);
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
                var deletedEntity = await _equityService.DeleteEquity(id);
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