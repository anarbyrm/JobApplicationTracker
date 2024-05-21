using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.ViewModels;

namespace JobApplicationTracker.Application.Services
{
    public interface IJobApplicationService
    {
        Task<List<JobApplicationListViewModel>> GetAllAsync(JobQueryModel query, PaginationModel pagination);
        Task<JobApplicationDetailViewModel> GetOneByIdAsync(Guid Id);
        Task<bool> CreateAsync(JobApplicationCreateModel createModel);
        Task<bool> UpdateAsync(Guid Id, JobApplicationUpdateModel updateModel);
        Task<bool> DeleteAsync(Guid Id);
    }
}
