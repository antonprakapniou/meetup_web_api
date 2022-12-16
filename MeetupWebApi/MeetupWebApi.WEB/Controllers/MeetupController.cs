using MeetupWebApi.BLL.DTO;
using MeetupWebApi.BLL.Exceptions;
using MeetupWebApi.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MeetupWebApi.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetupController:ControllerBase
    {
        private readonly IMeetupService _service;
        private readonly ILogger<MeetupController> _logger;

        public MeetupController(
            IMeetupService service,            
            ILogger<MeetupController> logger)
        {
            _service= service;
            _logger= logger;
        }

        [HttpGet]
        public async Task<Results<NotFound<string>, Ok<IEnumerable<MeetupDto>>, BadRequest<string>>> GetAllAsync()
        {
            try
            {
                var meetupDtoList = await _service.GetMeetupsAsync();
                _logger.LogInformation("Get request is completed successfully");
                return TypedResults.Ok(meetupDtoList);
            }

            catch (NonExistentObjectException ex)
            {
                _logger.LogError($"{ex.Message}");
                return TypedResults.NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Get request is failed");
                return TypedResults.BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<Results<NotFound<string>, Ok<MeetupDto>, BadRequest<string>>> GetMeetupAsyncById(int id)
        {
            try
            {
                var meetupDto = await _service.GetMeetupByIdAsync(id);
                _logger.LogInformation("Get request is completed successfully");
                return TypedResults.Ok(meetupDto);
            }

            catch (NonExistentObjectException ex)
            {
                _logger.LogError($"{ex.Message}");
                return TypedResults.NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Get request is failed");
                return TypedResults.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<Results<Created<MeetupDto>, Ok<MeetupDto>, BadRequest<string>,Conflict<string>>> PostMeetupAsync(MeetupDto meetupDto)
        {
            try
            {
                var meetupDtoFromCreate = await _service.CreateMeetupAsync(meetupDto);
                var location = Url.Action(nameof(GetMeetupAsyncById), new { id = meetupDtoFromCreate.Id }) ?? $"/{meetupDtoFromCreate.Id}";
                _logger.LogInformation("Post request is completed successfully");
                return TypedResults.Created(location, meetupDtoFromCreate);
            }

            catch (InvalidObjectException ex)
            {
                _logger.LogError($"{ex.Message}");
                return TypedResults.Conflict(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Post request is failed");
                return TypedResults.BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<Results<NotFound<string>, Ok<MeetupDto>, BadRequest<string>, Conflict<string>>> PutMeetupAsync(MeetupDto meetupDto)
        {
            try
            {
                var meetupDtoFromUpdate = await _service.UpdateMeetupAsync(meetupDto);
                _logger.LogInformation("Put request is completed successfully");
                return TypedResults.Ok(meetupDto);
            }

            catch (InvalidObjectException ex)
            {
                _logger.LogError($"{ex.Message}");
                return TypedResults.Conflict(ex.Message);
            }

            catch (NonExistentObjectException ex)
            {
                _logger.LogError($"{ex.Message}");
                return TypedResults.NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Put request is failed");
                return TypedResults.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<Results<NotFound<string>, Ok<MeetupDto>, BadRequest<string>>> DeleteMeetupAsync(MeetupDto meetupDto)
        {
            try
            {
                var meetupDtoFromUpdate = await _service.DeleteMeetupAsync(meetupDto);
                _logger.LogInformation("Delete request is completed successfully");
                return TypedResults.Ok(meetupDto);
            }

            catch (NonExistentObjectException ex)
            {
                _logger.LogError($"{ex.Message}");
                return TypedResults.NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete request is failed");
                return TypedResults.BadRequest(ex.Message);
            }
        }
    }
}