using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.ViewModels;

namespace JobApplicationTracker.Application.Services
{
    public interface IJobApplicationService
    {
        Task<List<JobApplicationListViewModel>> GetAllAsync(string query);
        Task<JobApplicationDetailViewModel> GetOneByIdAsync(Guid Id);
        Task<JobApplicationDetailViewModel> CreateAsync(JobApplicationCreateModel createModel);
        Task<JobApplicationDetailViewModel> UpdateAsync(Guid Id, JobApplicationUpdateModel updateModel);
        Task<JobApplicationDetailViewModel> DeleteAsync(Guid Id);
    }
}
