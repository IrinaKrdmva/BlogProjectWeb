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
            
            return View("Add");
        }
    }
}
