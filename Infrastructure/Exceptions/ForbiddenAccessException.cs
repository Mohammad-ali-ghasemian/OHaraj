namespace OHaraj.Infrastructure.Exceptions
{
    public class ForbiddenAccessException : ApplicationException
    {
        public ForbiddenAccessException(string message) : base(message)
        {
        }
        public ForbiddenAccessException() : base("دسترسی غیرمجاز")
        {
        }
    }
}
