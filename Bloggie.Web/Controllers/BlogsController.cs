using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogRepositories blogRepositories;
        private readonly IBlogPostLikeRepositories blogPostLikeRepositories;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(IBlogRepositories blogRepositories,IBlogPostLikeRepositories blogPostLikeRepositories,SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            this.blogRepositories = blogRepositories;
            this.blogPostLikeRepositories = blogPostLikeRepositories;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
           var blogPost= await blogRepositories.GetBlogPostByURLHandleAsync(urlHandle);
            if(signInManager.IsSignedIn(User))
            {
              var likesForBlog=  await blogPostLikeRepositories.GetLikesForBlog(blogPost.Id);

                var userId = userManager.GetUserId(User);
                if(userId!=null)
                {
                 var likeFromUser=   likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                    liked= likeFromUser!=null;
                }
            }
            var model = new BlogPostDetailsViewModel();
            if (blogPost != null)
            {
                var totalLikes = await blogPostLikeRepositories.GetBlogPostLikes(blogPost.Id);
                model.Author=blogPost.Author;
                model.ShortDescription=blogPost.ShortDescription;
                model.Likes=totalLikes;
                model.Visible=true;
                model.Tags  =blogPost.Tags;
                model.FeaturedImageUrl=blogPost.FeaturedImageUrl;
                model.PublishedDate=blogPost.PublishedDate;
                model.Content=blogPost.Content;
                model.Heading=blogPost.Heading;
                model.Id=blogPost.Id;
                model.UrlHandle=urlHandle;
                model.PageTitle=blogPost.PageTitle;
                model.Liked = liked;
            }
            return View(model);
        }
    }
}
