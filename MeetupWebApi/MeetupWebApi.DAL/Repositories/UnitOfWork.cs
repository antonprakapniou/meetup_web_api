using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Models;

namespace MeetupWebApi.DAL.Repositories
{
    //use to data transfer from repositories to buisness-logic-layer
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        private MeetupRepository? _meetupRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context=context;
        }

        public IGenericRepository<Meetup> MeetupRepository
        {
            get
            {
                if (_meetupRepository==null)
                    _meetupRepository=new MeetupRepository(_context);

                return _meetupRepository;
            }
        }        

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}