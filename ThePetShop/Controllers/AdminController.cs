using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThePetShop.Servises.Interface;
using ThePetShopApp.Models;

namespace ThePetShopApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFilteringService filteringService;
        private readonly ICommentService commentService;
        private readonly IAnimalService animalService;
        private readonly ICategoryService categoryService;
        private readonly IImageManager imageManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(
            IFilteringService filteringService,
            ICommentService commentService,
            IAnimalService animalService,
            ICategoryService dms,
            IImageManager imageManager,
            RoleManager<IdentityRole> rm)
        {
            this.filteringService = filteringService;
            this.commentService = commentService;
            this.animalService = animalService;
            this.categoryService = dms;
            this.imageManager = imageManager;
            this.roleManager = rm;
        }

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Index(int id = 0, string inputString = "", string inputSpesies = "")
        {
            if (inputString == null) inputString = "";
            ViewBag.Options = categoryService.GetCategories();
            ViewBag.InputString = inputString;
            ViewBag.Id = id;
           
            var animalList = filteringService.FilterAnimals(id, inputString, inputSpesies);
            ViewBag.Count = animalList.ToList().Count();
            return View(animalList);
        }
        


        // GET: Admin/Details/5
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int? id)
        {
            if (id == null || animalService.GetAnimals == null) return NotFound();

            var animal = animalService.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // GET: Admin/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "CategoryId", "CategoryId");
            ViewBag.Categories = categoryService.GetCategories();
            return View();
        }

        // POST: Admin/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AnimalId,Name,Species,Description,Age,PictureFile,CategoryId")] Animal animal)
        {
            if (animal.PictureFile != null)
                animal.PictureName = imageManager.CopyImage(animal);
            //validity checker, used to see what may be wrong with model
            //var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
                animalService.AddAnimal(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = categoryService.GetCategories();
            if (id == null || animalService.GetAnimals() == null) return NotFound();

            var animal = animalService.GetAnimalByID(id);
            if (animal == null) return NotFound();
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // POST: Admin/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AnimalId,Name,Species,Description,Age,PictureName,CategoryId")] Animal animal, IFormFile? picture)
        {
            ViewBag.Categories = categoryService.GetCategories();
            if (picture != null)
            {
                imageManager.UpdateImage(animal, picture);
            }
            if (id != animal.AnimalId) return NotFound();

            //validity checker, used to see what may be wrong with model
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                animalService.Update(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || animalService.GetAnimals() == null) return NotFound();

            var animal = animalService.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (animalService.GetAnimals() == null) return Problem("Entity set 'AnimalContext.AnimalList'  is null.");
            var animal = animalService.GetAnimalByID(id);
            if (animal!.PictureName != null)
                imageManager.DeleteImage(animal!.PictureName!);
            if (animal != null) animalService.RemoveAnimal(animal);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult DeleteComment(int? animalId, int? commentId)
        {
            ViewBag.Comment = commentService.GetCommentByID((int)commentId!);
            var animal = animalService.GetAnimalByID(animalId);
            return View(animal);
        }
        [Authorize]
        public IActionResult DeleteCommentConfirmed(int? animalId, int commentId)
        {
            commentService.DeleteComment((int)commentId!);
            return RedirectToAction("Details", new { id = animalId });
        }
    }
}
