namespace MeetupWebApi.BLL.Exceptions
{
    public class NullReturnableException:Exception
    {
        public NullReturnableException(string message) : base(message) { }
    }
}