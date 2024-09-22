using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface ITagRepositories
    {

       Task< IEnumerable<Tag> > GetAllAsync();

        Task<Tag?> GetTagAsync(Guid Id);

        Task<Tag?> UpdateTag(Tag tag);

        Task<Tag?> DeleteTagAsync(Tag tag);

        Task<Tag?> AddTagAsync(Tag tag);

        Task<Tag?> DeleteTagAsyncById(Guid Id);


    }
}
