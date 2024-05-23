namespace JobApplicationTracker.Core.Enums
{
    public enum ApplicationStatus
    {
        Applied = 1, // initial application
        Rejected,
        Accepted,
        OnGoing // next stage after application
    }
}
