using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
namespace SRMWebApiApp.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class FileController: ControllerBase {
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile csvFile) {
            if (csvFile.Length < 0) {
                return BadRequest("400 Not File Attached");
            } 

            var cleanFileName = Path.GetFileName(csvFile.FileName);
            var pathName = Path.Combine(Path.Combine(@"C:\\Users\\ysbavishi\\Documents\\Case Study\\Security Master\\SRM_Backend\\File\\"), cleanFileName);
           // var filePath = Path.Combine(Path.Combine(@"C:\\Users\\ysbavishi\\Documents\\Case Study\\Security Master\backend\\WebApi\\File\\"), cleanFileName);
            var stream = System.IO.File.Create(pathName);
            await csvFile.CopyToAsync(stream);

            return Ok("200 File Saved");

        }
    }
}