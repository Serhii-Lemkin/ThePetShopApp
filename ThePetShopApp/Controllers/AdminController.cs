using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Data;
using ThePetShopApp.Models;
using ThePetShopApp.Servises;

namespace ThePetShopApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDataManagerService dms;
        private readonly IImageManager imageManager;

        public AdminController( IWebHostEnvironment hostEnvironment , IDataManagerService dms, IImageManager imageManager)
        {
            _hostEnvironment = hostEnvironment;
            this.dms = dms;
            this.imageManager = imageManager;
        }

        // GET: Admin
        public IActionResult Index(int? id = 0)
        {
            ViewBag.Options = dms.GetCategories();
            if (id == 0)
            {
                var animalContext = dms.GetAnimals();
                return View(animalContext);
            }
            else
            {
                var animalContext = dms.GetAnimalsOfCategoryByID(id);
                return View(animalContext);
            }
        }

        // GET: Admin/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || dms.GetAnimals == null) return NotFound();

            var animal = dms.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(dms.GetCategories(), "CategoryId", "CategoryId");
            ViewBag.Categories = dms.GetCategories();
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AnimalId,Name,Description,Age,PictureFile,CategoryId")] Animal animal)
        {
            if (animal.PictureFile != null)
            {
                //Storing Image
                animal.PictureName = imageManager.AddImage(animal.PictureFile!, out string path);
                //string wwwrootPath = _hostEnvironment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(animal.PictureFile!.FileName);
                //string extention = Path.GetExtension(animal.PictureFile.FileName);
                //animal.PictureName = fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extention;
                //string path = Path.Combine(wwwrootPath + "/pictures/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    animal.PictureFile!.CopyToAsync(fileStream);
                }
            }
            

            //validity checker, used to see what may be wrong with model
            //var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
                

                dms.AddAnimal(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(dms.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = dms.GetCategories();
            if (id == null || dms.GetAnimals() == null) return NotFound();

            var animal = dms.GetAnimalByID(id);
            if (animal == null) return NotFound();
            ViewData["CategoryId"] = new SelectList(dms.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Description,Age,PictureName,CategoryId")] Animal animal)
        {
            if (id != animal.AnimalId) return NotFound();


            //validity checker, used to see what may be wrong with model
            //var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
                dms.Update(animal);                
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(dms.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dms.GetAnimals() == null) return NotFound();

            var animal = dms.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dms.GetAnimals() == null) return Problem("Entity set 'AnimalContext.AnimalList'  is null.");
            var animal = dms.GetAnimalByID(id);
            if (animal!.PictureName != null)
                imageManager.DeleteImage(animal!.PictureName!);
            if (animal != null) dms.Remove(animal);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteComment(int? animalId, int? commentId)
        {
            ViewBag.Comment = dms.GetCommentByID((int)commentId!);
            var animal = dms.GetAnimalByID(animalId);
            return View(animal);
        }
        public IActionResult DeleteCommentConfirmed(int? animalId, int commentId)
        {
            dms.DeleteComment((int)commentId!);            
            return RedirectToAction("Details", new { id = animalId });
        }
    }
}
