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
        public async Task<IActionResult> List([FromQuery] JobQueryModel query, PaginationModel pagination)
        {
            var applications = await _service.GetAllAsync(query, pagination);
            return View(applications);
        }

        [Route("{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var application = await _service.GetOneByIdAsync(id);
            return View(application);
        }

        [HttpGet("add")]
        public IActionResult Create()
        {
            var newApplication = new JobApplicationCreateModel();
            return View(newApplication);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(JobApplicationCreateModel jobApplication)
        {
            var app = await _service.CreateAsync(jobApplication);
            return RedirectToAction("List");
        }
    }
}
