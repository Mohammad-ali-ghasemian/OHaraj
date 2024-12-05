namespace Project.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string item) : base($"{item}")
        {
        }
        public NotFoundException() : base("اطلاعات مورد نظر پیدا نشد")
        {
        }
    }
}
