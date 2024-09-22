using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepositories : ITagRepositories
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public TagRepositories(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }
        async Task<Tag?> ITagRepositories.AddTagAsync(Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();

            return tag;
        }

       async Task<Tag?> ITagRepositories.DeleteTagAsync(Tag tag)
        {
            var tagtodelete = await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (tagtodelete != null)
           {
               _bloggieDbContext.Tags.Remove(tagtodelete); 
            await   _bloggieDbContext.SaveChangesAsync();
                return tagtodelete;
            }
            else
            {
                return null;
            }
        }

        async Task<IEnumerable<Tag>> ITagRepositories.GetAllAsync()
        {
           var tag= await _bloggieDbContext.Tags.ToListAsync();
            return tag;
        }

       async  Task<Tag?> ITagRepositories.GetTagAsync(Guid Id)
        {
            var tag = await _bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == Id);
            if(tag!=null)
            {
                return tag;
            }
            return null;
        }

      async  Task<Tag?> ITagRepositories.UpdateTag(Tag tag)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);
            if(existingTag!=null)
            {

                existingTag.DisplayName = tag.DisplayName;
                existingTag.Name  = tag.Name;
               await  _bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
           
        }

        async Task<Tag?> ITagRepositories.DeleteTagAsyncById(Guid Id)
        {
          var tag=  await _bloggieDbContext.Tags.FindAsync(Id);
            if(tag!=null)
            {
                _bloggieDbContext.Tags.Remove(tag);
               await _bloggieDbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }
    }
}
