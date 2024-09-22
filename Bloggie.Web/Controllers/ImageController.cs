using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepositories imageRepositories;

        public ImageController(IImageRepositories imageRepositories)
        {
            this.imageRepositories = imageRepositories;
        }
        [HttpPost]
       public async Task<IActionResult> UploadAsync(IFormFile formfile)
        {
          var imageURL= await imageRepositories.UploadAsync(formfile);
            if(imageURL != null)
            {
                return new JsonResult(new {link= imageURL });
            }
            return Problem("Something went wromg",null,(int)HttpStatusCode.InternalServerError);
        }
    }
}
