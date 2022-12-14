using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MeetupWebApi.BLL.DTO;
using MeetupWebApi.BLL.Exceptions;
using MeetupWebApi.BLL.Interfaces;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MeetupWebApi.BLL.Services
{
    //use for data transfer to data-presentation-layer 
    public class MeetupService:IMeetupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MeetupDto> _validator;
        private readonly ILogger<MeetupService> _logger;

        public MeetupService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IValidator<MeetupDto> validator,
            ILogger<MeetupService> logger)
        {
            _mapper=mapper;
            _unitOfWork=unitOfWork;
            _validator=validator;
            _logger=logger;
        }

        //throw NonExistentObjectException if meetup list is empty or null
        public async Task<IEnumerable<MeetupDto>> GetMeetupsAsync()
        {
            var meetupDtos = await _unitOfWork.Meetup.GetAllAsync();

            if (meetupDtos.Count()==0||meetupDtos==null)
            {
                string errorMessage = $"No objects found";
                _logger.LogDebug(errorMessage);
                throw new NonExistentObjectException(errorMessage);
            }

            return _mapper.Map<IEnumerable<MeetupDto>>(meetupDtos);
        }

        //throw NonExistentObjectException if meetup doesn't exist
        public async Task<MeetupDto> GetMeetupByIdAsync(int id)
        {
            var meetups = await _unitOfWork
                .Meetup
                .GetByAsync(u => u.Id==id);

            MeetupDto meetupDto = _mapper.Map<MeetupDto>(meetups.FirstOrDefault());

            if (meetupDto == null)
            {
                string errorMessage = $"Meetup with id = {id} doesn't exist";
                _logger.LogDebug(errorMessage);
                throw new NonExistentObjectException(errorMessage);
            }

            return meetupDto;
        }

        //throw NonExistentObjectException if meetup doesn't exist
        //throw InvalidObjectException if meetupDto doesn't valid
        public async Task<MeetupDto> CreateMeetupAsync(MeetupDto meetupDto)
        {
            ValidationResult result=await _validator.ValidateAsync(meetupDto);

            if (result.IsValid)
            {
                var meetupForCreate = _mapper.Map<Meetup>(meetupDto);
                await _unitOfWork.Meetup.AddAsync(meetupForCreate);

                try
                {
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogDebug("CreateMeetupAsync operation completed successfully");
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreateMeetupAsync operation is failed");
                }
            }

            else throw new InvalidObjectException("MeetupDto model isn't valid");           

            return meetupDto;
        }

        //throw NonExistentObjectException if meetup doesn't exist
        //throw InvalidObjectException if meetupDto doesn't valid
        public async Task<MeetupDto> UpdateMeetupAsync(MeetupDto meetupDto)
        {
            var meetupForUpdate = _mapper.Map<Meetup>(meetupDto);
            var meetups = await _unitOfWork.Meetup.GetByAsync(m => m.Id==meetupForUpdate.Id);

            if (meetups.Count()==0)
            {
                string errorMessage = $"Meetup with id = {meetupDto.Id} doesn't exist";
                _logger.LogDebug(errorMessage);
                throw new NonExistentObjectException(errorMessage);
            }

            else
            {
                ValidationResult result = await _validator.ValidateAsync(meetupDto);
                if (result.IsValid)
                {                   
                    _unitOfWork.Meetup.Update(meetupForUpdate);

                    try
                    {
                        await _unitOfWork.SaveChangesAsync();
                        _logger.LogDebug("UpdateMeetupAsync operation completed successfully");
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "UpdateMeetupAsync operation is failed");
                    }
                }

                else
                {
                    throw new InvalidObjectException("MeetupDto model isn't valid");
                }                
            }

            return meetupDto;
        }

        //throw NonExistentObjectException if meetup doesn't exist
        public async Task<MeetupDto> DeleteMeetupAsync(MeetupDto meetupDto)
        {
            var meetupForDelete = _mapper.Map<Meetup>(meetupDto);
            var meetups = await _unitOfWork.Meetup.GetByAsync(m => m.Id==meetupForDelete.Id);

            if (meetups.Count()==0)
            {
                string errorMessage = $"Meetup with id = {meetupDto.Id} doesn't exist";
                _logger.LogDebug(errorMessage);
                throw new NonExistentObjectException(errorMessage);
            }

            else
            {
                _unitOfWork.Meetup.Delete(meetupForDelete);

                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "DeleteMeetupAsync operation is failed");
                }

                return meetupDto;
            }
        }
    }
}