namespace MeetupWebApi.BLL.Exceptions
{
    public class NonExistentObjectException:Exception
    {
        public NonExistentObjectException(string message):base(message) { }
    }
}