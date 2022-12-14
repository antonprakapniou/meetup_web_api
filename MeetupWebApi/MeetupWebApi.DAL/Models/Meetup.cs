using System.ComponentModel.DataAnnotations;

namespace MeetupWebApi.DAL.Models
{
    public class Meetup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Topic { get; set; }

        public string? Description { get; set; }
        public string? Schedule { get; set; }
        public string? Sponsor { get; set; }
        public string? Speaker { get; set; }

        [Required]
        public string? Address { get; set; }

        //use converting to Utc for work with PostgreSQL 
        [Required]
        public DateTime? Spending { get; set; }
    }
}