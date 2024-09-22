using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogRepositories
    {
        Task<BlogPost?> GetBlogPostByURLHandleAsync(string urlHandle);
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetBlogPost(Guid id);

        Task<BlogPost?> EditBlogPostAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteBlogPostAsync(Guid id);

        

        Task<BlogPost> AddBlogPostAsync(BlogPost post);
        
    }
}
