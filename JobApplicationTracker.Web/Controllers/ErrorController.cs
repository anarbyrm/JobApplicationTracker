using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Web.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }

        [Route("not-found")]
        public IActionResult ItemOrPageNotFound()
        {
            return View();
        }

    }
}
