using JobApplicationTracker.Core.Enums;

namespace JobApplicationTracker.Application.ViewModels
{
    public class JobApplicationDetailViewModel
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedAt { get; set; }
        public string Note {  get; set; }
    }
}
