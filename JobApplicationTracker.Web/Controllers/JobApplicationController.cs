using FluentValidation.Results;
using JobApplicationTracker.Application.Exceptions;
using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.Services;
using JobApplicationTracker.Application.Validators;
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
        public async Task<IActionResult> List([FromQuery] JobQueryModel query, [FromQuery] PaginationModel pagination)
        {
            try
            {
                List<JobApplicationListViewModel> applications = await _service.GetAllAsync(query, pagination);
                return View(applications);
            }
            catch
            {
                ModelState.AddModelError("", "Unexpected error occured, please retry again later.");
                return View();
            }
        }

        [Route("{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                JobApplicationDetailViewModel? application = await _service.GetOneByIdAsync(id);
                return application is null 
                    ? throw new ItemNotFoundException($"Job application with ID: {id} not found.") 
                    : (IActionResult)View(application);
            }
            catch (Exception exc)
            {
                string errorMessage = "Unexpected error occured, please retry again later.";
                if (exc is ItemNotFoundException)
                    errorMessage = exc.Message;

                ModelState.AddModelError("", errorMessage);
                return View();
            }
        }

        [HttpGet("add")]
        public IActionResult Create()
        {
            JobApplicationCreateModel newApplication = new();
            return View(newApplication);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(JobApplicationCreateModel jobApplication, [FromServices] JobApplicationCreateValidator validator)
        {
            var result = await validator.ValidateAsync(jobApplication);
            if (!result.IsValid)
            {
                AddErrorMessages(result.Errors);
                return View(jobApplication);
            }

            try
            {
                await _service.CreateAsync(jobApplication);
                TempData["SuccessMessage"] = "Application is successfully added";
            }
            catch
            {
                ModelState.AddModelError("", "Unexpected error occured, please retry again later.");
                return View(jobApplication);
            }

            return RedirectToAction("List");
        }

        [NonAction]
        private void AddErrorMessages(List<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
