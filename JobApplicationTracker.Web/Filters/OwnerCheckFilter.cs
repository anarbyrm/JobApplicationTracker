using JobApplicationTracker.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JobApplicationTracker.Web.Filters
{
    public class OwnerCheckFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var service = context.HttpContext.RequestServices.GetService<IJobApplicationService>();
            string currentUserId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            context.HttpContext.Request.RouteValues.TryGetValue("id", out var applicationId);

            var itemGuid = Guid.Parse(applicationId.ToString());
            var application = await service.GetOneByIdAsync(itemGuid);

            if (application != null && application.UserId != currentUserId)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();

        }
    }
}
