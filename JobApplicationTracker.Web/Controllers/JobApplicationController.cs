using FluentValidation.AspNetCore;
using JobApplicationTracker.Application.Exceptions;
using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.Services;
using JobApplicationTracker.Application.Validators;
using JobApplicationTracker.Application.ViewModels;
using JobApplicationTracker.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Web.Controllers
{
    [Authorize]
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
            catch (Exception exc)
            {
                return RedirectToErrorPage(exc);
            }
        }

        [Route("{id}")]
        [OwnerCheckFilter]
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                JobApplicationDetailViewModel? application = await GetApplicationOrThrowException(id); 
                return View(application);
            }
            catch (Exception exc)
            {
                return RedirectToErrorPage(exc);
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
            catch (Exception exc)
            {
                return RedirectToErrorPage(exc);
            }

            return RedirectToAction("List");
        }

        [HttpPost("{id}/remove")]
        [OwnerCheckFilter]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (Exception exc)
            {
                return RedirectToErrorPage(exc);
            }
            return RedirectToAction("List");
        }

        [HttpGet("{id}/edit")]
        [OwnerCheckFilter]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                JobApplicationDetailViewModel application = await GetApplicationOrThrowException(id);
                return View(application);
            }
            catch (Exception exc)
            {
                return RedirectToErrorPage(exc);
            }
        }

        [HttpPost("{id}/edit")]
        [OwnerCheckFilter]
        public async Task<IActionResult> Edit(
            Guid id, 
            [FromForm] JobApplicationUpdateModel updatedJobApplication,
            [FromServices] JobApplicationUpdateValidator validator)
        {
            try
            {
                var result = await validator.ValidateAsync(updatedJobApplication);

                if (!result.IsValid)
                {
                    result.AddToModelState(ModelState);
                    var application = await GetApplicationOrThrowException(id);
                    return View("Edit", application);
                }
            
                await _service.UpdateAsync(id, updatedJobApplication);
                TempData["SuccesMessage"] = $"Update action for application ID: {id} is successful";
            }
            catch (Exception exc)
            {
                return RedirectToErrorPage(exc);
            }
            return RedirectToAction("Detail", new { id });
        }

        private IActionResult RedirectToErrorPage(Exception exc)
        {
            string errorMessage = "Unexpected error occured, please retry again later.";
            string actionName = "Index";

            if (exc is ItemNotFoundException)
            {
                errorMessage = exc.Message;
                actionName = "ItemOrPageNotFound";
            }

            TempData["ErrorMessage"] = errorMessage;
            return RedirectToAction(actionName, "Error");
        }

        private async Task<JobApplicationDetailViewModel> GetApplicationOrThrowException(Guid id)
        {
            var application = await _service.GetOneByIdAsync(id);

            if (application is null)
                throw new ItemNotFoundException($"Job application with ID: {id} not found.");

            return application;
        }
    }
}
