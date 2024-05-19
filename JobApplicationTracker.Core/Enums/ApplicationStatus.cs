namespace JobApplicationTracker.Core.Enums
{
    public enum ApplicationStatus
    {
        Applied, // initial application
        Rejected,
        Accepted,
        OnGoing // next stage after application
    }
}
