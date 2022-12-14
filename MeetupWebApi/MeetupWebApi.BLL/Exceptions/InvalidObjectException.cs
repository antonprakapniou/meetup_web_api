namespace MeetupWebApi.BLL.Exceptions
{
    public class InvalidObjectException:Exception
    {
        public InvalidObjectException(string message):base(message) { }
    }
}