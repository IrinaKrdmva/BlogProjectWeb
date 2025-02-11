using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using BlogProjectWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectWeb.Controllers {
    public class AdminTagsController : Controller {
        private readonly BlogDbContext blogDbContext;

        public AdminTagsController(BlogDbContext blogDbContext) {
            this.blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) {

            //Mapping the AddTagRequest to Tag domain model
            var tag = new Tag {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await blogDbContext.Tags.AddAsync(tag);
            await blogDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() {
            //Use DbContext to read the Tags
            var tags = await blogDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            //1st method
            //var tag = blogDbContext.Tags.Find(id);

            //2nd method
            var tag = await blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag != null) {
                var editTagRequest = new EditTagRequest {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) {

            var tag = new Tag {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = await blogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null) {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //Save changes
                await blogDbContext.SaveChangesAsync();

                //Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            //Show error notificaion
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) {
        
            var tag = await blogDbContext.Tags.FindAsync(editTagRequest.Id);

            if (tag != null) {
                blogDbContext.Tags.Remove(tag);
                await blogDbContext.SaveChangesAsync();

                //Show success notification
                return RedirectToAction("List");
            }

            //Show error notificaion
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
 