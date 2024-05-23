using JobApplicationTracker.Core.Enums;

namespace JobApplicationTracker.Application.Models
{
    public class JobApplicationCreateModel
    {
        public string? CompanyName { get; set; }
        public string? Position { get; set; }
        public ApplicationStatus? Status { get; set; }
        public DateTime? AppliedAt { get; set; }
        public string? Note { get; set; }
    }
}
