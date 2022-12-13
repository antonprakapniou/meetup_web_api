namespace MeetupWebApi.WEB.Exceptions
{
    //use if the specified db connection is not found
    public class InvalidDbConnectionException:Exception
    {
        public InvalidDbConnectionException(string message):base(message) { }
    }
}