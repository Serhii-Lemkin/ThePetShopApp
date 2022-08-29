using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThePetShopApp.Data;
using ThePetShopApp.Models;
using ThePetShopApp.Servises;

namespace ThePetShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataManagerService dms;

        public HomeController(ILogger<HomeController> logger, IDataManagerService dms)
        {
            _logger = logger;
            this.dms = dms;
        }

        public IActionResult Index() => View(dms.GetMostPopular(2));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}