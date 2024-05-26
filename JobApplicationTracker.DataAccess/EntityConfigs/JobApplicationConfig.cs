using JobApplicationTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplicationTracker.DataAccess.EntityConfigs
{
    public class JobApplicationConfig : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.Property(ja => ja.CompanyName)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(ja => ja.Position)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(ja => ja.Note)
                .HasMaxLength(500);
            // job application - user relationship (foreign key)
            builder
                .HasOne(ja => ja.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
