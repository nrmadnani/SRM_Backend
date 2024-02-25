using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using SRMWebApiApp.Services;
namespace SRMWebApiApp.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class FileController: ControllerBase {
        private readonly IFileReadService _fileService;

        public FileController(IFileReadService fileService) {
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile csvFile) {
            if (csvFile.Length < 0) {
                return BadRequest("400 Not File Attached");
            } 

            var cleanFileName = Path.GetFileName(csvFile.FileName);
            var pathName = Path.Combine(Path.Combine(@"C:\\Users\\ysbavishi\\Documents\\Case Study\\Security Master\\SRM_Backend\\File\\"), cleanFileName);
            using (var stream = System.IO.File.Create(pathName)){
                await csvFile.CopyToAsync(stream);
            };
            Console.WriteLine("DEBUG");
            FileStream filestream = new FileStream(pathName, FileMode.Open);
            Console.WriteLine("DEBUGa");
            var obj = await _fileService.UploadData(filestream);

            return Ok(obj);

        }
    }
}