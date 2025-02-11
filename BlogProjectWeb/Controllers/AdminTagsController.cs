using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using BlogProjectWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add(AddTagRequest addTagRequest) {

            //Mapping the AddTagRequest to Tag domain model
            var tag = new Tag {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            blogDbContext.Tags.Add(tag);
            blogDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List() {
            //Use DbContext to read the Tags
            var tags = blogDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id) {
            //1st method
            //var tag = blogDbContext.Tags.Find(id);

            //2nd method
            var tag = blogDbContext.Tags.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Edit(EditTagRequest editTagRequest) {

            var tag = new Tag {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = blogDbContext.Tags.Find(tag.Id);

            if (existingTag != null) {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //Save changes
                blogDbContext.SaveChanges();

                //Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            //Show error notificaion
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest) {
        
            var tag = blogDbContext.Tags.Find(editTagRequest.Id);

            if (tag != null) {
                blogDbContext.Tags.Remove(tag);
                blogDbContext.SaveChanges();

                //Show success notification
                return RedirectToAction("List");
            }

            //Show error notificaion
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
