namespace MeetupWebApi.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IMeetupRepository Meetup { get; }
        public Task SaveChangesAsync();
    }
}