using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.ViewModels;

namespace JobApplicationTracker.Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        public Task<List<JobApplicationListViewModel>> GetAllAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<JobApplicationDetailViewModel> GetOneByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<JobApplicationDetailViewModel> CreateAsync(JobApplicationCreateModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task<JobApplicationDetailViewModel> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<JobApplicationDetailViewModel> UpdateAsync(Guid Id, JobApplicationUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
