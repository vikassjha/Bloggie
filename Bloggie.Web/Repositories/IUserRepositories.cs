using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Repositories
{
    public interface IUserRepositories
    {

      Task<IEnumerable<IdentityUser>>  GetAll();
    }
}
