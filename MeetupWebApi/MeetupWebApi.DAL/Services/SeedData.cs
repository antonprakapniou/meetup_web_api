using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeetupWebApi.DAL.Services
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context.Meetups.Any())
                {
                    return;   // DB has been seeded
                }

               context.AddRange(
                                    new Meetup
                                    {
                                        Name="Meetup_test_1",
                                        Topic="Topic_test_1",
                                        Description="Description_test_1",
                                        Schedule=new List<string> { "point_test_1.1", "point_test_1.2", "point_test_1.3" },
                                        Sponsors=new List<string> { "sponsor_test_1.1", "sponsor_test_1.2", "sponsor_test_1.3" },
                                        Speakers=new List<string> { "speaker_test_1.1", "speaker_test_1.2", "speaker_test_1.3" },
                                        Address="Address_test_1",
                                        Spending=DateTime.UtcNow
                                    },

                                    new Meetup
                                    {
                                        Name="Meetup_test_2",
                                        Topic="Topic_test_2",
                                        Description="Description_test_1",
                                        Schedule=new List<string> { "point_test_2.1", "point_test_2.2", "point_test_2.3" },
                                        Sponsors=new List<string> { "sponsor_test_2.1", "sponsor_test_2.2", "sponsor_test_2.3" },
                                        Speakers=new List<string> { "speaker_test_2.1", "speaker_test_2.2", "speaker_test_2.3" },
                                        Address="Address_test_2",
                                        Spending=DateTime.UtcNow
                                    },

                                    new Meetup
                                    {
                                        Name="Meetup_test_3",
                                        Topic="Topic_test_3",
                                        Description="Description_test_3",
                                        Schedule=new List<string> { "point_test_3.1", "point_test_3.2", "point_test_3.3" },
                                        Sponsors=new List<string> { "sponsor_test_3.1", "sponsor_test_3.2", "sponsor_test_3.3" },
                                        Speakers=new List<string> { "speaker_test_3.1", "speaker_test_3.2", "speaker_test_3.3" },
                                        Address="Address_test_1",
                                        Spending=DateTime.UtcNow
                                    });
                context.SaveChanges();
            }
        }
    }
}