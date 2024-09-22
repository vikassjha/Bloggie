namespace Bloggie.Web.Repositories
{
    public interface IImageRepositories
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
