using Bloggie.Web.Data;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagController : Controller
    {
        private readonly ITagRepositories tagRepositories;

        //private readonly BloggieDbContext _bloggieDbContext;

        //public AdminTagController(BloggieDbContext bloggieDbContext)
        //{
        //    this._bloggieDbContext = bloggieDbContext;
        //}

        public AdminTagController(ITagRepositories tagRepositories)
        {
            this.tagRepositories = tagRepositories;
        }
       
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Models.Domain.Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName

            };
            await tagRepositories.AddTagAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var tags = await tagRepositories.GetAllAsync();

            return View(tags);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid Id)
        {
            var tag = await tagRepositories.GetTagAsync(Id);


            if (tag != null)
            {
                EditTagRequest editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName

                };
                return View(editTagRequest);

            }
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {

            var tag = new Models.Domain.Tag
            {
                Name = editTagRequest.Name,
                Id = editTagRequest.Id,
                DisplayName = editTagRequest.DisplayName

            };
            var updatedTag = await tagRepositories.UpdateTag(tag);
            if (updatedTag != null)
            {

                return RedirectToAction("List");
            }
            else
            {
                return View(editTagRequest);
            }


        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
           var tag= await tagRepositories.DeleteTagAsyncById(id);
            

            return RedirectToAction("List");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = new Models.Domain.Tag
            {
                Name = editTagRequest.Name,
                Id = editTagRequest.Id,
                DisplayName = editTagRequest.DisplayName

            };

            var tags = await tagRepositories.DeleteTagAsync(tag);
            
            

            return RedirectToAction("List");
        }
    }
}
