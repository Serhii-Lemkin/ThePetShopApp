using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Data;
using ThePetShopApp.Models;

namespace ThePetShopApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AnimalContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(AnimalContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin
        public async Task<IActionResult> Index(int? id = 0)
        {
            ViewBag.Options = _context.GetCategories();
            if (id == 0)
            {
                var animalContext = _context.GetAnimalsWithCategories();
                return View(animalContext);
            }
            else
            {
                var animalContext = _context.GetAnimalsOfCategoryByID(id);
                return View(animalContext);
            }
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalList == null) return NotFound();

            var animal = _context.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId");
            ViewBag.Categories = _context.GetCategories();
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Name,Description,Age,PictureFile,CategoryId")] Animal animal)
        {
            //Storing Image
            string wwwrootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(animal.PictureFile!.FileName);
            string extention = Path.GetExtension(animal.PictureFile.FileName);
            animal.PictureName = fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extention;
            string path = Path.Combine(wwwrootPath + "/pictures/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await animal.PictureFile.CopyToAsync(fileStream);
            }
            //validity checker, used to see what may be wrong with model
            //var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categories = _context.GetCategories();
            if (id == null || _context.AnimalList == null) return NotFound();

            var animal = await _context.AnimalList.FindAsync(id);
            if (animal == null) return NotFound();
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId", animal.CategoryId);
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
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimalList == null) return NotFound();

            var animal = _context.GetAnimalByID(id);
            if (animal == null) return NotFound();

            return View(animal);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimalList == null) return Problem("Entity set 'AnimalContext.AnimalList'  is null.");
            var animal = await _context.AnimalList.FindAsync(id);
            //Delete image from root
            var picPath = Path.Combine(_hostEnvironment.WebRootPath, "pictures", animal!.PictureName!);

            if (System.IO.File.Exists(picPath)) System.IO.File.Delete(picPath);
            if (animal != null) _context.AnimalList.Remove(animal);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteComment(int? animalId, int? commentId)
        {
            ViewBag.Comment = _context.GetCommentByID((int)commentId!);
            var animal = _context.GetAnimalByID(animalId);
            return View(animal);
        }
        public IActionResult DeleteCommentConfirmed(int? animalId, int commentId)
        {
            _context.DeleteComment((int)commentId!);            
            return RedirectToAction("Details", new { id = animalId });
        }

        private bool AnimalExists(int id) => (_context.AnimalList?.Any(e => e.AnimalId == id)).GetValueOrDefault();


    }
}
