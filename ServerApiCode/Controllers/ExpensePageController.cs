using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ServerApiCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensePageController : ControllerBase
    {
        private readonly ILogger<UploadFileController> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ExpensePageController(ILogger<UploadFileController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }
    }
}
