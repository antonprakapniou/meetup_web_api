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

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Schedule { get; set; }

        [Required]
        public string? Sponsor { get; set; }

        [Required]
        public string? Speaker { get; set; }

        [Required]
        public string? Adress { get; set; }

        //use converting to Utc for work with PostgreSQL 
        [Required]
        public DateTime? Spending { get; set; }
    }
}