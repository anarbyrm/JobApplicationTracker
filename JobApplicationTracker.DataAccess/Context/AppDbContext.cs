using JobApplicationTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobApplicationTracker.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<JobApplication> Applications { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
