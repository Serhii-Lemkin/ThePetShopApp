using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThePetShopApp.Servises;

namespace ThePetShopApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDataManagerService dms;
        private readonly UserManager<IdentityUser> users;

        public AnimalsController(
            IWebHostEnvironment hostEnvironment, 
            IDataManagerService dms, 
            UserManager<IdentityUser> users)
        {
            _hostEnvironment = hostEnvironment;
            this.dms = dms;
            this.users = users;
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
        [Authorize]
        public IActionResult CreateComment(int? receivedId, string commentText)
		{
            
            string id = users.GetUserId(HttpContext.User);
            ViewBag.userId = id;
            if (!String.IsNullOrEmpty(commentText) && !String.IsNullOrWhiteSpace(commentText) && commentText.Length < 250 )
            {
                dms.AddCommentToAnimal((int)receivedId!, commentText, id);
            }
            var animal = dms.GetAnimalByID(receivedId);
            return RedirectToAction("Details", new { id = receivedId });
		}
    }
}
