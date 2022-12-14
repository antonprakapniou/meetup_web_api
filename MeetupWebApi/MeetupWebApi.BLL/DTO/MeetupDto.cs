namespace MeetupWebApi.BLL.DTO
{
    public class MeetupDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Topic { get; set; }
        public string? Description { get; set; }
        public string? Schedule { get; set; }
        public string? Sponsor { get; set; }
        public string? Speaker { get; set; }
        public string? Address { get; set; }

        //use converting to Utc for work with PostgreSQL 
        public DateTime? Spending { get; set; }
    }
}