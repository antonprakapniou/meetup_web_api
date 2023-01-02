using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Interfaces;

namespace MeetupWebApi.DAL.Repositories
{
    //use to data transfer from repositories to buisness-logic-layer
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IMeetupRepository Meetup {get;}

        public UnitOfWork(AppDbContext context)
        {
            _context=context;
            Meetup=new MeetupRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}