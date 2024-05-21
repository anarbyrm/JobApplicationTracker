using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
