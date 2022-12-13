using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Exceptions;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MeetupWebApi.DAL.Repositories
{
    public class MeetupRepository:GenericRepository<Meetup>,IMeetupRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MeetupRepository> _logger;

        public MeetupRepository(AppDbContext context,ILogger<MeetupRepository> logger) : base(context)
        {
            _logger = logger;
            if (context== null)
            {
                string errorMessage = "Db context is null";
                _logger.LogError(errorMessage);
                throw new DbContextNullException(errorMessage);
            }
            _context = context;            
        }
    }
}