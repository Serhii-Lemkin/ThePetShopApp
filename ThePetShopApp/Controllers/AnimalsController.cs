﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Data;
using ThePetShopApp.Models;

namespace ThePetShopApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AnimalContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimalsController(AnimalContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Animals
        public async Task<IActionResult> Index(int id = 0)
        {

            ViewBag.Options = _context.CategoryList!.ToList();

            if (id == 0)
            {
                var animalContext = _context.AnimalList!.Include(a => a.Categories).ToList();
                return View(animalContext);
            }
            else
            {
                var animalContext = _context.AnimalList!.Where(a => a.CategoryId == id).Include(a => a.Categories).ToList();
                return View(animalContext);
            }

        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalList == null)
            {
                return NotFound();
            }

            var animal = await _context.AnimalList
                .Include(a => a.Categories)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Name,Description,Age,PictureFile,CategoryId")] Animal animal)
        {
            //copy and save image
            string wwwrootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(animal.PictureFile!.FileName);
            string extention = Path.GetExtension(animal.PictureFile.FileName);
            animal.PictureName = fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extention;
            string path = Path.Combine(wwwrootPath + "/pictures/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await animal.PictureFile.CopyToAsync(fileStream);
            }
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimalList == null)
            {
                return NotFound();
            }

            var animal = await _context.AnimalList.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Description,Age,PictureFile,CategoryId")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryList, "CategoryId", "CategoryId", animal.CategoryId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimalList == null)
            {
                return NotFound();
            }

            var animal = await _context.AnimalList
                .Include(a => a.Categories)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.AnimalList == null)
            {
                return Problem("Entity set 'AnimalContext.AnimalList'  is null.");
            }
            var animal = await _context.AnimalList.FindAsync(id);
            //Delete image from root
            var picPath = Path.Combine(_hostEnvironment.WebRootPath, "pictures", animal!.PictureName!);
            if (System.IO.File.Exists(picPath))
            {
                System.IO.File.Delete(picPath);
            }
            if (animal != null)
            {
                _context.AnimalList.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return (_context.AnimalList?.Any(e => e.AnimalId == id)).GetValueOrDefault();
        }
    }
}
