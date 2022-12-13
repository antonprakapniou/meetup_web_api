namespace MeetupWebApi.DAL.Exceptions
{
    public class DbContextNullException:Exception
    {
        public DbContextNullException(string message):base(message) { }
    }
}