using AutoMapper;
using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.ViewModels;
using JobApplicationTracker.Core.Entities;
using JobApplicationTracker.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace JobApplicationTracker.Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpContext? _httpContext;

        public JobApplicationService(
            IJobApplicationRepository repository,
            IMapper mapper,
            IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContext = contextAccessor.HttpContext;
        }

        public async Task<(List<JobApplicationListViewModel>, int)> GetAllAndCountAsync(
            JobQueryModel query, PaginationModel pagination)
        {
            string? currentUserId = GetCurrentUserId();
            var predicate = BuildPredicateExpression(currentUserId, query);
            var (applications, totalItemCount) = await _repository.GetAllAndCountAsync(
                predicate: predicate,
                limit: pagination.Limit, 
                offset: pagination.Offset);

            var resultApplications = _mapper.Map<List<JobApplicationListViewModel>>(applications);
            return (resultApplications, totalItemCount);
        }

        public async Task<JobApplicationDetailViewModel> GetOneByIdAsync(Guid Id)
        {
            var application = await _repository.GetFirstAsync(ja => ja.Id == Id);
            return _mapper.Map<JobApplicationDetailViewModel>(application);
        }

        public async Task<bool> CreateAsync(JobApplicationCreateModel createModel)
        {               
            var newApplication = _mapper.Map<JobApplication>(createModel);
            newApplication.UserId = GetCurrentUserId();
            bool isDone = await _repository.AddAsync(newApplication);
            return isDone;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var application = await _repository.GetFirstAsync(ja => ja.Id == Id );

            if (application is null)
                return false;

            return await _repository.DeleteAsync(application);
        }

        public async Task<bool> UpdateAsync(Guid Id, JobApplicationUpdateModel updateModel)
        {
            var application = await _repository.GetFirstAsync(ja => ja.Id == Id);

            if (application is null)
                return false;

            _mapper.Map(updateModel, application);
            return await _repository.Update(application);
        }

        private string? GetCurrentUserId()
        {
            var userId = _httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        private Expression<Func<JobApplication, bool>> BuildPredicateExpression(
            string? currentUserId, JobQueryModel query)
        {
            // example:
            // ja => ja.User.Id == currentUserId
            // && ja.Position.Contains(query.Position)
            // && ja.CompanyName.Contains(query.CompanyName)

            ParameterExpression parameter = Expression.Parameter(typeof(JobApplication), "ja");

            // user Id expression tree building
            var userIdConst = Expression.Constant(currentUserId, typeof(string));
            var userParam = Expression.Property(parameter, "User");
            var idParam = Expression.Property(userParam, "Id");
            var expressionBody = Expression.Equal(idParam, userIdConst);

            // job application position expression tree building
            if (!string.IsNullOrWhiteSpace(query.Position))
            {
                var positionProp = Expression.Property(parameter, "Position");
                var methidInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var positionConst = Expression.Constant(query.Position, typeof(string));
                var positionContainsExpr = Expression.Call(positionProp, methidInfo, positionConst);
                expressionBody = Expression.AndAlso(expressionBody, positionContainsExpr);
            }

            // job application company name expression tree building
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                var companyNameProp = Expression.Property(parameter, "CompanyName");
                var methidInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var companyNameConst = Expression.Constant(query.CompanyName, typeof(string));
                var companyNameContainsExpr = Expression.Call(companyNameProp, methidInfo, companyNameConst);
                expressionBody = Expression.AndAlso(expressionBody, companyNameContainsExpr);
            }

            return Expression.Lambda<Func<JobApplication, bool>>(expressionBody, parameter);
        }
    }
}
