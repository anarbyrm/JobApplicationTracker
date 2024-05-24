using JobApplicationTracker.Core.Entities;
using JobApplicationTracker.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobApplicationTracker.DataAccess.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public JobApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<JobApplication>, int)> GetAllAndCountAsync(
            Expression<Func<JobApplication, bool>> predicate, 
            int limit,
            int offset)
        {
            IQueryable<JobApplication> query = _dbContext.Applications;
            int totalItemCount = query.Count();
            if (predicate != null)
                query = query.Where(predicate);
            var applications = await query
                .OrderByDescending(ja => ja.CreatedOn)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return (applications, totalItemCount);
        }

        public async Task<JobApplication?> GetFirstAsync(Expression<Func<JobApplication, bool>> predicate)
        {
            return await _dbContext.Applications.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AddAsync(JobApplication entity)
        {
            await _dbContext.Applications.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> Update(JobApplication entity)
        {
            _dbContext.Applications.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(JobApplication entity)
        {
            _dbContext.Applications.Remove(entity);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
