namespace JobApplicationTracker.Application.Models
{
    public class PaginationModel
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
