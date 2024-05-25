using FluentValidation.AspNetCore;
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
        private readonly IJobApplicationService _service;

        public JobApplicationController(IJobApplicationService service)
        {
            _service = service;
        }

        [Route("", Name = "item-list")]
        public async Task<IActionResult> List(
            [FromQuery] JobQueryModel query, 
            [FromQuery] PaginationModel pagination)
        {
            try
            {
                var (applications, totalItemCount) = await _service.GetAllAndCountAsync(query, pagination);
                ViewBag.TotalItemCount = totalItemCount;
                return View(applications);
            }
            catch
            {
                // todo: implement proper error page
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
        public async Task<IActionResult> Create(
            [FromForm] JobApplicationCreateModel jobApplication, 
            [FromServices] JobApplicationCreateValidator validator)
        {
            var result = await validator.ValidateAsync(jobApplication);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
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

        [HttpPost("{id}/remove")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch
            {
                ModelState.AddModelError("", "Unexpected error occured, please retry again later.");
                return View();
            }
            return RedirectToAction("List");
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var application = await _service.GetOneByIdAsync(id);
            if (application is null)
                // redirect to error page
                return View();
            return View(application);
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Edit(
            Guid id, 
            [FromForm] JobApplicationUpdateModel updatedJobApplication,
            [FromServices] JobApplicationUpdateValidator validator)
        {
            var result = await validator.ValidateAsync(updatedJobApplication);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                var application = await _service.GetOneByIdAsync(id);
                return View("Edit", application);
            }
            try
            {
                await _service.UpdateAsync(id, updatedJobApplication);
                TempData["SuccesMessage"] = $"Update action for application ID: {id} is successful";
            }
            catch
            {
                ModelState.AddModelError("", "Unexpected error occured, please retry again later.");
                return View();
            }
            return RedirectToAction("Detail", new { id });
        }
    }
}
