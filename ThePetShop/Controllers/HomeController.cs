using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThePetShopApp.Models;
using ThePetShop.Servises.Interface;

namespace ThePetShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFilteringService filteringService;
        public HomeController(IFilteringService filteringService) => this.filteringService = filteringService;



        public IActionResult Index() => View(filteringService.FilterAnimalsMostPopular(2));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}