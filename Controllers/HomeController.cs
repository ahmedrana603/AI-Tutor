using Microsoft.AspNetCore.Mvc;
using AITutorWebsite.Models;

namespace AITutorWebsite.Controllers
{
    public class HomeController : Controller
    {
        // ===== Home Page =====
        public IActionResult Index()
        {
            return View();
        }

        // ===== Privacy Page =====
        public IActionResult Privacy()
        {
            return View();
        }

        // ===== Contact Page =====
        public IActionResult Contact()
        {
            return View();
        }

        // ===== Error Page =====
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier
            };
            return View(model);
        }
    }
}
