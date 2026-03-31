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

        public EventController()
        {
            IRepository<Event> repo = new FileRepository();
            _eventService = new EventService(repo);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_eventService.GetByCategory(1));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ev = _eventService.GetById(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        public IActionResult Create(Event ev)
        {
            _eventService.CreateEvent(ev);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Event ev)
        {
            _eventService.UpdateEvent(ev);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _eventService.DeleteEvent(id);
            return Ok();
        }
    }
}