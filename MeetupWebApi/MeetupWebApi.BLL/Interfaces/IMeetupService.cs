using MeetupWebApi.BLL.DTO;

namespace MeetupWebApi.BLL.Interfaces
{
    public interface IMeetupService
    {
        public Task<IEnumerable<MeetupDto>> GetMeetupsAsync();

        public Task<MeetupDto> GetMeetupByIdAsync(int id);

        public Task<MeetupDto> CreateMeetupAsync(MeetupDto meetupDto);

        public Task<MeetupDto> UpdateMeetupAsync(MeetupDto meetupDto);

        public Task<MeetupDto> DeleteMeetupAsync(MeetupDto meetupDto);
    }
}