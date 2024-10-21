using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ServerApiCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly ILogger<UploadFileController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UploadFileController(ILogger<UploadFileController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {

                var httpContent = HttpContext.Request;

                if (httpContent is null)
                    return BadRequest();

                if (httpContent.Form.Files.Count > 0)
                {
                    foreach (var file in httpContent.Form.Files)
                    {
                        var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "ImagesUploadFolder");

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            System.IO.File.WriteAllBytes(Path.Combine(filePath, file.FileName), memoryStream.ToArray());
                        }
                    }
                    return Ok(httpContent.Form.Files.Count.ToString() + " file(s) uploaded");
                }
                return BadRequest("No File Selected");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult Get(string ImageName)
        {
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "ImagesUploadFolder");
            try
            {
                //Byte[] b = System.IO.File.ReadAllBytes(@"C:\Users\nate\source\repos\TechnicianAllInOne\ServerApiCode\ImagesUploadFolder\1_10-15-24-11-49-30.jpeg");   // You can use your own method over here.         
                Byte[] b = System.IO.File.ReadAllBytes(Path.Combine(filePath, ImageName));   // You can use your own method over here.         
                                                                                             //"1_10-15-24-11-49-30.jpeg"
                return File(b, "image/jpeg");
            }
            catch
            {
                return new NotFoundResult();
            }
        }
        //////////////////////////////////////////////////////////////////////////////
    }
}
