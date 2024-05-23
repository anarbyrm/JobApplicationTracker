namespace JobApplicationTracker.Application.Exceptions
{
    public class ItemNotFoundException : Exception 
    {
        public ItemNotFoundException(string? errorMessage) : base(errorMessage)
        {

        }
    }
}
