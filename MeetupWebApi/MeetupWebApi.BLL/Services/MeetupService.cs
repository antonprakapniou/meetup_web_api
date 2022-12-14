﻿using AutoMapper;
using FluentValidation;
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

        //possible null returning
        //throw NullReturnableException if meetup list is empty
        public async Task<IEnumerable<MeetupDto>> GetMeetupsAsync()
        {
            var meetupDtos = await _unitOfWork.MeetupRepository.GetAllAsync();

            if (meetupDtos == null)
            {
                _logger.LogDebug("MeetupDto collection is empty");
                throw new NullReturnableException($"GetMeetupAsync return null");
            }

            return _mapper.Map<IEnumerable<MeetupDto>>(meetupDtos);
        }

        //possible null returning
        //throw NonExistentObjectException if meetup doesn't exist
        public async Task<MeetupDto> GetMeetupByIdAsync(int id)
        {
            var meetups = await _unitOfWork
                .MeetupRepository
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
        public async Task<MeetupDto> CreateMeetupAsync(MeetupDto meetupDto)
        {
            var meetupForCreate = _mapper.Map<Meetup>(meetupDto);
            await _unitOfWork.MeetupRepository.AddAsync(meetupForCreate);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                _logger.LogDebug("CreateMeetupAsync operation completed successfully");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateMeetupAsync operation is failed");
            }

            return meetupDto;
        }

        //throw NonExistentObjectException if meetup doesn't exist
        public async Task<MeetupDto> UpdateMeetupAsync(MeetupDto meetupDto)
        {
            if (_unitOfWork.MeetupRepository.GetByAsync(m => m.Id==meetupDto.Id)==null)
            {
                string errorMessage = $"Meetup with id = {meetupDto.Id} doesn't exist";
                _logger.LogDebug(errorMessage);
                throw new NonExistentObjectException(errorMessage);
            }

            else
            {
                var meetupForUpdate = _mapper.Map<Meetup>(meetupDto);
                _unitOfWork.MeetupRepository.Update(meetupForUpdate);

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

            return meetupDto;
        }

        //throw NonExistentObjectException if meetup doesn't exist
        public async Task<MeetupDto> DeleteMeetupAsync(MeetupDto meetupDto)
        {
            if (_unitOfWork.MeetupRepository.GetByAsync(m => m.Id==meetupDto.Id)==null)
            {
                string errorMessage = $"Meetup with id = {meetupDto.Id} doesn't exist";
                _logger.LogDebug(errorMessage);
                throw new NonExistentObjectException(errorMessage);
            }

            else
            {
                var meetupForDelete = _mapper.Map<Meetup>(meetupDto);
                _unitOfWork.MeetupRepository.Delete(meetupForDelete);

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