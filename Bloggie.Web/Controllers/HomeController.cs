using Bloggie.Web.Models;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepositories blogRepositories;
        private readonly ITagRepositories tagRepositories;

        public HomeController(ILogger<HomeController> logger,IBlogRepositories blogRepositories,ITagRepositories tagRepositories)
        {
            _logger = logger;
            this.blogRepositories = blogRepositories;
            this.tagRepositories = tagRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogPosts =await  blogRepositories.GetAllAsync();
            var tags=await tagRepositories.GetAllAsync();
            var model =new HomeViewModel {
                blogPosts= blogPosts,
                tags=tags

            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
