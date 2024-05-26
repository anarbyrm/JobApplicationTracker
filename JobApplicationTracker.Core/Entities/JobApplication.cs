using JobApplicationTracker.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationTracker.Core.Entities
{
    public class JobApplication : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime AppliedAt { get; set; }
        public ApplicationStatus Status { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
