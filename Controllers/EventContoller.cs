using Microsoft.AspNetCore.Mvc;
using EventApi.Models;
using EventApi.Services;
using EventApi.Data;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_eventService.GetAll());
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë marrjes së eventeve.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var ev = _eventService.GetById(id);
                if (ev == null)
                    return NotFound("Eventi nuk u gjet");
                return Ok(ev);
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë kërkimit.");
            }
        }

        [HttpPost]
        public IActionResult Create(Event ev)
        {
            try
            {
                var (success, message) = _eventService.CreateEvent(ev);
                return success ? Ok(message) : BadRequest(message);
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë krijimit.");
            }
        }

        [HttpPut]
        public IActionResult Update(Event ev)
        {
            try
            {
                var (success, message) = _eventService.UpdateEvent(ev);
                if (!success && message == "Itemi nuk u gjet")
                    return NotFound(message);
                return success ? Ok(message) : BadRequest(message);
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë përditësimit.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var (success, message) = _eventService.DeleteEvent(id);
                if (!success && message == "Itemi nuk u gjet")
                    return NotFound(message);
                return success ? Ok(message) : BadRequest(message);
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë fshirjes.");
            }
        }

        // 📊 STATS (FEATURE E RE)
        [HttpGet("stats")]
        public IActionResult Stats()
        {
            try
            {
                return Ok(_eventService.GetPriceStats());
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë kalkulimit të statistikave.");
            }
        }

        // ↕️ SORT (FEATURE E RE)
        // Example: /api/event/sort?by=price&dir=asc
        [HttpGet("sort")]
        public IActionResult Sort(string? by, string? dir)
        {
            try
            {
                return Ok(_eventService.Sort(by, dir));
            }
            catch
            {
                return StatusCode(500, "Gabim gjatë sortimit.");
            }
        }
    }
}