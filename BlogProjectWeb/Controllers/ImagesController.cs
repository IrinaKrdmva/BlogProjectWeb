using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase {

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file) {
            //To all a repository

        }
    }
}
