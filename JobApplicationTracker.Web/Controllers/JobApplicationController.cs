using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.Services;
using JobApplicationTracker.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Web.Controllers
{
    [Route("applications")]
    public class JobApplicationController : Controller
    {
        private IJobApplicationService _service;

        public JobApplicationController(IJobApplicationService service)
        {
            _service = service;
        }

        [Route("")]
        public async Task<IActionResult> List()
        {
            //List<JobApplicationListViewModel> applications = await _service.GetAllAsync();
            return View();
        }

        [Route("{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var application = await _service.GetOneByIdAsync(id);
            return View(application);
        }
    }
}
