using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogRepositories blogRepositories;

        public BlogsController(IBlogRepositories blogRepositories)
        {
            this.blogRepositories = blogRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
           var blogPost= await blogRepositories.GetBlogPostByURLHandleAsync(urlHandle);
            return View(blogPost);
        }
    }
}
