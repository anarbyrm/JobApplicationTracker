using JobApplicationTracker.Core.Entities;
using JobApplicationTracker.DataAccess.DbContexts;
using System.Linq.Expressions;

namespace JobApplicationTracker.DataAccess.Repositories
{
    public class ApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public ApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<JobApplication>> GetAllAsync(Expression<Func<JobApplication, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<JobApplication> GetFirstAsync(Expression<Func<JobApplication, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(JobApplication entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(JobApplication entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(JobApplication entity)
        {
            throw new NotImplementedException();
        }
    }
}
