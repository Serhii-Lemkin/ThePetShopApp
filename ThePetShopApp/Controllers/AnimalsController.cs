using Microsoft.AspNetCore.Mvc;
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

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalList == null) return NotFound();
            var animal = _context.GetAnimalByID(id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        public IActionResult CreateComment(int? receivedId, string commentText)
		{
            if (!String.IsNullOrEmpty(commentText) && !String.IsNullOrWhiteSpace(commentText) || commentText.Length > 250)
            {
                _context.AddCommentToAnimal((int)receivedId!, commentText);
            }
            var animal = _context.GetAnimalByID(receivedId);
            return RedirectToAction("Details", new { id = receivedId });
		}

        private bool AnimalExists(int id) => (_context.AnimalList?.Any(e => e.AnimalId == id)).GetValueOrDefault();
    }
}
