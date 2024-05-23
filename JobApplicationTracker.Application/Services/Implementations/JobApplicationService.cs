using AutoMapper;
using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.ViewModels;
using JobApplicationTracker.Core.Entities;
using JobApplicationTracker.DataAccess.Repositories;

namespace JobApplicationTracker.Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IMapper _mapper;

        public JobApplicationService(IJobApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // TODO: Implement exception handling
        // TODO: null value handling
        public async Task<(List<JobApplicationListViewModel>, int)> GetAllAndCountAsync(JobQueryModel query, PaginationModel pagination)
        {
            // TODO: build predicate based on query
            var (applications, totalItemCount) = await _repository.GetAllAndCountAsync(
                predicate: null, 
                limit: pagination.Limit, 
                offset: pagination.Offset);
            return (_mapper.Map<List<JobApplicationListViewModel>>(applications), totalItemCount);
        }

        public async Task<JobApplicationDetailViewModel> GetOneByIdAsync(Guid Id)
        {
            var application = await _repository.GetFirstAsync(ja => ja.Id == Id);
            return _mapper.Map<JobApplicationDetailViewModel>(application);
        }

        public async Task<bool> CreateAsync(JobApplicationCreateModel createModel)
        {               
            var newApplication = _mapper.Map<JobApplication>(createModel);
            var isDone = await _repository.AddAsync(newApplication);
            return isDone;
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Guid Id, JobApplicationUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
