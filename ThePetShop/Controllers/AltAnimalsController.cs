using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThePetShop.Servises.Interface;

namespace ThePetShop.Controllers
{
    public class AltAnimalsController : Controller
    {
        private readonly IFilteringService filteringService;
        private readonly ICommentService commentService;
        private readonly IAnimalService animalService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<IdentityUser> users;
        public AltAnimalsController(
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
        public IActionResult Details(int? id, string actionToUse)
        {
            if (id == null || animalService.GetAnimals() == null) return NotFound();
            var animal = animalService.GetAnimalByID(id);
            if (animal == null) return NotFound();
            return View(animal);
        }
    }
}
