using MeetupWebApi.BLL.DTO;
using MeetupWebApi.BLL.Exceptions;
using MeetupWebApi.BLL.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MeetupWebApi.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}