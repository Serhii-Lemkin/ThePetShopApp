using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThePetShopApp.Models;
using ThePetShopApp.Servises;

namespace ThePetShopApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDataManagerService dms;
        private readonly IImageManager imageManager;
        private readonly RoleManager<IdentityRole> rm;

        public AdminController(
            IDataManagerService dms, 
            IImageManager imageManager, 
            RoleManager<IdentityRole> rm)
        {
            this.dms = dms;
            this.imageManager = imageManager;
            this.rm = rm;
        }

        // GET: Admin
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int? id)
        {
            if (id == null || dms.GetAnimals == null) return NotFound();

            var animal = dms.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // GET: Admin/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(dms.GetCategories(), "CategoryId", "CategoryId");
            ViewBag.Categories = dms.GetCategories();
            return View();
        }

        // POST: Admin/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AnimalId,Name,Description,Age,PictureFile,CategoryId")] Animal animal)
        {
            if (animal.PictureFile != null)
                animal.PictureName = imageManager.CopyImage(animal);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AnimalId,Name,Description,Age,PictureName,CategoryId")] Animal animal, IFormFile? picture)
        {
            ViewBag.Categories = dms.GetCategories();
            if (picture != null)
            {
                imageManager.UpdateImage(animal, picture);
            }
            if (id != animal.AnimalId) return NotFound();
            
            //validity checker, used to see what may be wrong with model
            var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
                dms.Update(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(dms.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || dms.GetAnimals() == null) return NotFound();

            var animal = dms.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (dms.GetAnimals() == null) return Problem("Entity set 'AnimalContext.AnimalList'  is null.");
            var animal = dms.GetAnimalByID(id);
            if (animal!.PictureName != null)
                imageManager.DeleteImage(animal!.PictureName!);
            if (animal != null) dms.Remove(animal);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult DeleteComment(int? animalId, int? commentId)
        {
            ViewBag.Comment = dms.GetCommentByID((int)commentId!);
            var animal = dms.GetAnimalByID(animalId);
            return View(animal);
        }
        [Authorize]
        public IActionResult DeleteCommentConfirmed(int? animalId, int commentId)
        {
            dms.DeleteComment((int)commentId!);
            return RedirectToAction("Details", new { id = animalId });
        }
    }
}
