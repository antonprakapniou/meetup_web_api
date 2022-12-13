using MeetupWebApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetupWebApi.DAL.EF
{
    public class AppDbContext:DbContext
    {
        public DbSet<Meetup> Meetups { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
    }
}