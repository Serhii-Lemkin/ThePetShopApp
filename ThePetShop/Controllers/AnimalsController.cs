using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThePetShop.Servises.Interface;

namespace ThePetShopApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IFilteringService filteringService;
        private readonly ICommentService commentService;
        private readonly IAnimalService animalService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<IdentityUser> users;

        public AnimalsController(
            IFilteringService filteringService,
            ICommentService commentService,
            IAnimalService animalService,
            ICategoryService dms, 
            UserManager<IdentityUser> users)
        {
            this.filteringService = filteringService;
            this.commentService = commentService;
            this.animalService = animalService;
            this.categoryService = dms;
            this.users = users;
        }

        // GET: Animals
        public IActionResult Index(int id = 0, string inputString = "")
        {
            ViewBag.Options = categoryService.GetCategories();
            ViewBag.InputString = inputString ?? string.Empty;
            ViewBag.Id = id;
            if (id == 0 && inputString == "")
            {
                var animalContext = animalService.GetAnimals();
                ViewBag.Count = animalContext.ToList().Count();
                return View(animalContext);
            }
            else
            {
                var animalContext = filteringService.FilterAnimals(id, inputString);
                ViewBag.Count = animalContext.ToList().Count();
                return View(animalContext);
            }
        }
        

        // GET: Animals/Details/5
        public IActionResult Details(int? id, string actionToUse)
        {
            if (id == null || animalService.GetAnimals() == null) return NotFound();
            var animal = animalService.GetAnimalByID(id);
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
                commentService.AddCommentToAnimal((int)receivedId!, commentText, id);
            }
            var animal = animalService.GetAnimalByID(receivedId);
            return RedirectToAction("Details", new { id = receivedId });
		}
    }
}
