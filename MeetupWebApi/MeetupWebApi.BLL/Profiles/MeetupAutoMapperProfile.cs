using AutoMapper;
using MeetupWebApi.BLL.DTO;
using MeetupWebApi.DAL.Models;

namespace MeetupWebApi.BLL.Profiles
{
    public class MeetupAutoMapperProfile:Profile
    {
        public MeetupAutoMapperProfile()
        {
            CreateMap<Meetup, MeetupDto>().ReverseMap();
        }
    }
}