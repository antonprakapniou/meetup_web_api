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
        public List<string>? Schedule { get; set; }
        public List<string>? Sponsors { get; set; }
        public List<string>? Speakers { get; set; }

        [Required]
        public string? Address { get; set; }

        //use converting to Utc for work with PostgreSQL 
        [Required]
        public DateTime? Spending { get; set; }
    }
}