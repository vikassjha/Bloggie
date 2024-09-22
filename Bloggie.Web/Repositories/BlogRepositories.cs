using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogRepositories : IBlogRepositories
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogRepositories(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddBlogPostAsync(BlogPost post)
        {
           await  bloggieDbContext.BlogPosts.AddAsync(post);
            bloggieDbContext.SaveChanges();
            return post;
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid id)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlog != null)
            {
                bloggieDbContext.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;

            }
            return null;
        }

       

        public async Task<BlogPost?> EditBlogPostAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Author = blogPost.Author;
                existingBlog.Heading= blogPost.Heading;
                existingBlog.Content= blogPost.Content;
                existingBlog.Tags= blogPost.Tags;
                existingBlog.PublishedDate  = blogPost.PublishedDate;
                existingBlog.Visible= blogPost.Visible;
                existingBlog.FeaturedImageUrl= blogPost.FeaturedImageUrl;
                existingBlog.PageTitle= blogPost.PageTitle;
                existingBlog.UrlHandle= blogPost.UrlHandle;
                existingBlog.ShortDescription= blogPost.ShortDescription;
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           var BlogList= await bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
            return BlogList;
        }

        public async Task<BlogPost?> GetBlogPost(Guid id)
        {
            var BlogPost= await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id==id);
            return BlogPost;
        }

       async Task<BlogPost> IBlogRepositories.GetBlogPostByURLHandleAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }
    }
}
