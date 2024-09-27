using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostLikeRepositories
    {
        Task<int> GetBlogPostLikes(Guid BlogPostId);

        Task<BlogPostLike> AddBlogPostLike(BlogPostLike blogPostLike);

       Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
    }
}
