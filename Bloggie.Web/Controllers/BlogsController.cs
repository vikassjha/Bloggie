using Bloggie.Web.Models.Domain;
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
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogsController(IBlogRepositories blogRepositories,IBlogPostLikeRepositories blogPostLikeRepositories,SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogRepositories = blogRepositories;
            this.blogPostLikeRepositories = blogPostLikeRepositories;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
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
            var blogComments = await blogPostCommentRepository.GetCommentByBlogIdAsync(blogPost.Id);
            var blogCommentsForView = new List<BlogComment>();
            foreach(var blogComment in blogComments)
            {
                blogCommentsForView.Add(new BlogComment { DateAdded = blogComment.DateAdded,
                    Description=blogComment.Description,
                    Username=(await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName });
            }
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
                model.Comments = blogCommentsForView;
            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Index(BlogPostDetailsViewModel blogPostDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment

                {

                    BlogPostId = blogPostDetailsViewModel.Id,
                    Description = blogPostDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded=DateTime.Now

                };
              await  blogPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Blogs",new {urlHandle=blogPostDetailsViewModel.UrlHandle});
            }
            return View();
        }
    }
}
