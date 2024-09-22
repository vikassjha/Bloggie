using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepositories tagRepositories;
        private readonly IBlogRepositories blogRepositories;

        public AdminBlogPostController(ITagRepositories tagRepositories,IBlogRepositories blogRepositories)
        {
            this.tagRepositories = tagRepositories;
            this.blogRepositories = blogRepositories;
        }
        [HttpGet]
        
        public async Task<IActionResult> Add()
        {
          var model= await  tagRepositories.GetAllAsync();

            var blogPostRequest = new AddBlogPostRequest
            {
                Tags = model.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text=x.Name ,Value= x.Id.ToString()})
            };
            
            return View(blogPostRequest);
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            var blogPostDomainModel = new BlogPost { 
            

                Heading=addBlogPostRequest.Heading,
                FeaturedImageUrl=addBlogPostRequest.FeaturedImageUrl,
                ShortDescription=addBlogPostRequest.ShortDescription,
                PublishedDate=addBlogPostRequest.PublishedDate,
                Author=addBlogPostRequest.Author,
                Content=addBlogPostRequest.Content,
                Visible=addBlogPostRequest.Visible,
                PageTitle=addBlogPostRequest.PageTitle,
                UrlHandle=addBlogPostRequest.UrlHandle
            };
            List<Tag> tags = new List<Tag>();
            foreach(var tagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagId = Guid.Parse(tagId);
              var selectedTag=  await tagRepositories.GetTagAsync(selectedTagId);
                tags.Add(selectedTag);
            }
            blogPostDomainModel.Tags = tags;

          var addedTag= await  blogRepositories.AddBlogPostAsync(blogPostDomainModel);
            return RedirectToAction("List");
        }

        [HttpGet]

        public  async Task<IActionResult> List()
        {

            var tag = await blogRepositories.GetAllAsync();
            return View(tag);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
          var blogPost=  await blogRepositories.GetBlogPost(Id);
            var tagsDomainModel = await tagRepositories.GetAllAsync();
            var blogPostViewModel = new EditBlogPostRequest
            {
               Heading= blogPost.Heading,
               UrlHandle = blogPost.UrlHandle,
               Content  =blogPost.Content,
               ShortDescription=blogPost.ShortDescription,
               PageTitle=blogPost.PageTitle,
               FeaturedImageUrl=blogPost.FeaturedImageUrl,
               Visible=blogPost.Visible,
               Author=blogPost.Author,
               PublishedDate=blogPost.PublishedDate,
               Id=Id,
               Tags= tagsDomainModel.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name , Value=x.Id.ToString() }),

               SelectedTags= blogPost.Tags.Select(x=>x.Id.ToString()).ToArray() 

            };

            return View(blogPostViewModel);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
         {
            var blogPostDomainModel = new BlogPost 
            {
                Heading = editBlogPostRequest.Heading,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                ShortDescription = editBlogPostRequest.ShortDescription,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Author = editBlogPostRequest.Author,
                Content = editBlogPostRequest.Content,
                Visible = editBlogPostRequest.Visible,
                PageTitle = editBlogPostRequest.PageTitle,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Id=editBlogPostRequest.Id
            };
            List<Tag> tags = new List<Tag>();
            foreach (var tagId in editBlogPostRequest.SelectedTags)
            {
                var selectedTagId = Guid.Parse(tagId);
                var selectedTag = await tagRepositories.GetTagAsync(selectedTagId);
                tags.Add(selectedTag);
            }
            blogPostDomainModel.Tags = tags;
           await blogRepositories.EditBlogPostAsync(blogPostDomainModel);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            await blogRepositories.DeleteBlogPostAsync(editBlogPostRequest.Id);

            return RedirectToAction("List");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid Id)
        {
            await blogRepositories.DeleteBlogPostAsync(Id);

            return RedirectToAction("List");
        }
    }
}
