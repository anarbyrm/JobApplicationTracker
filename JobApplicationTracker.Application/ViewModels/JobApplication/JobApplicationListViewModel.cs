using JobApplicationTracker.Core.Enums;

namespace JobApplicationTracker.Application.ViewModels
{
    public class JobApplicationListViewModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
