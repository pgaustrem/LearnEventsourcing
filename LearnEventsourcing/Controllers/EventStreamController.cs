using Microsoft.AspNetCore.Mvc;

namespace LearnEventsourcing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventStreamController : ControllerBase
    {
        private readonly ILogger<EventStreamController> _logger;

        public EventStreamController(ILogger<EventStreamController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public EventStream Get()
        {
            return new EventStream();
        }
    }
}
