using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using BlogProjectWeb.Models.ViewModels;
using BlogProjectWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectWeb.Controllers {
    public class AdminTagsController : Controller {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository) {
            this.tagRepository = tagRepository;
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

            //Calls the methods in the repository (add and save to the db)
            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() {
            //Use the repository to access the db and read the Tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            //Call the tag repository for one tag search by id
            var tag = await tagRepository.GetAsync(id);

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

            //Calling the repository for the Update method
            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null) {
                //Show success notificaion
            } else {
                //Show error notificaion
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) {
        
            //Call the tag repository to access the Delete method
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null) {
                //Show success notificaion
                return RedirectToAction("List");
            } 

            //Show error notificaion
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
 