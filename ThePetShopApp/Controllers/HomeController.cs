using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThePetShopApp.Data;
using ThePetShopApp.Models;

namespace ThePetShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnimalContext _context;

        public HomeController(ILogger<HomeController> logger, AnimalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() => View(_context.GetMostPopular(2));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}