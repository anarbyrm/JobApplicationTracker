using JobApplicationTracker.Core.Enums;

namespace JobApplicationTracker.Application.Models
{
    public class JobApplicationUpdateModel
    {
        public string Note { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
