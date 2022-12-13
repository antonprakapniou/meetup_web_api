using MeetupWebApi.DAL.Models;

namespace MeetupWebApi.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IGenericRepository<Meetup> MeetupRepository { get; }
        public Task SaveChangesAsync();
    }
}