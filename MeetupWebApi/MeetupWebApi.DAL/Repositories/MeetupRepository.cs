using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Models;

namespace MeetupWebApi.DAL.Repositories
{
    public class MeetupRepository:GenericRepository<Meetup>,IMeetupRepository
    {
        private readonly AppDbContext _context;

        public MeetupRepository(AppDbContext context) : base(context)
        {            
            _context = context;            
        }
    }
}