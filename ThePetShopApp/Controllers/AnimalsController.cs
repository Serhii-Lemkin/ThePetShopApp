using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThePetShopApp.Data;
using ThePetShopApp.Models;
using ThePetShopApp.Repositories;
using ThePetShopApp.Servises;

namespace ThePetShopApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDataManagerService dms;

        public AnimalsController(IWebHostEnvironment hostEnvironment, IDataManagerService dms)
        {
            _hostEnvironment = hostEnvironment;
            this.dms = dms;
        }

        // GET: Animals
        public ActionResult Index(int id = 0)
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

        // GET: Animals/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || dms.GetAnimals() == null) return NotFound();
            var animal = dms.GetAnimalByID(id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        public IActionResult CreateComment(int? receivedId, string commentText)
		{
            if (!String.IsNullOrEmpty(commentText) && !String.IsNullOrWhiteSpace(commentText) && commentText.Length > 250 )
            {
                dms.AddCommentToAnimal((int)receivedId!, commentText);
            }
            var animal = dms.GetAnimalByID(receivedId);
            return RedirectToAction("Details", new { id = receivedId });
		}
    }
}
