using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepositories blogPostLikeRepositories;

        public BlogPostLikeController(IBlogPostLikeRepositories blogPostLikeRepositories)
        {
            this.blogPostLikeRepositories = blogPostLikeRepositories;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] BlogPostLike blogPostLike)
        {
            await blogPostLikeRepositories.AddBlogPostLike(blogPostLike);

            return Ok();
        }


        [HttpGet]
        [Route("{blogPostId}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
          var totalLikes=  await blogPostLikeRepositories.GetBlogPostLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}
