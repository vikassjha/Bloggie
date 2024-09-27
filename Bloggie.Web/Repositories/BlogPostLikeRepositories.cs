
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepositories : IBlogPostLikeRepositories
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepositories(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike> AddBlogPostLike(BlogPostLike blogPostLike)
        {
             await bloggieDbContext.BlogPostsLikes.AddAsync(blogPostLike);
          await  bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        async public Task<int> GetBlogPostLikes(Guid BlogPostId)
        {
           return await bloggieDbContext.BlogPostsLikes.CountAsync(x=>x.BlogPostId == BlogPostId);
        }

      async  public Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostsLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
