using AutoMapper;
using EventManagment.context;
using EventManagment.Dtos;
using EventManagment.Models;
using EventManagment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetEvents();
            return Ok(events);
        }
     

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var evnt = await _eventService.GetEventById(id);
            if (evnt == null)
                return NotFound();

            return Ok(evnt);
        }


        //[HttpPost("CreateEvent")]
        //public async Task<IActionResult> CreateEvent(Event newEvent)
        //{
        //    try
        //    {
        //        var createdEvent = await _eventService.CreateEvent(newEvent);
        //        return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }

        //}
        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventDto newEventDto)
        {
            try
            {
                var newEvent = new Event
                {
                    EventName = newEventDto.EventName,
                    Date = newEventDto.Date,
                    Time = newEventDto.Time,
                    Location = newEventDto.Location,
                    Description = newEventDto.Description,
                    UserId = newEventDto.UserId,
                    Budget = new Budget
                    {
                        Expenses = newEventDto.Budget.Expenses,
                        Revenue = newEventDto.Budget.Revenue
                    },
                    Guests = newEventDto.Guests.Select(g => new Guest { /* Map guest properties here */ }).ToList()
                };

                var createdEvent = await _eventService.CreateEvent(newEvent);
                return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateEventWithBudget")]
        public async Task<IActionResult> CreateEventWithBudget([FromBody] EventDto eventDto)
        {
            try
            {
                var newEvent = _mapper.Map<Event>(eventDto); 
                var createdEvent = await _eventService.CreateEventWithBudget(newEvent);
                return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateEvent/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
        {
            if (id != updatedEvent.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _eventService.UpdateEvent(updatedEvent);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
     
        [HttpGet("{eventId}/guests")]
        public async Task<IActionResult> GetGuestsForEvent(int eventId)
        {
            var guests = await _eventService.GetGuestsForEvent(eventId);
            return Ok(guests);
        }

        [HttpPost("{eventId}/guests")]
        public async Task<IActionResult> AddGuestToEvent(int eventId, Guest newGuest)
        {
            var createdGuest = await _eventService.AddGuestToEvent(eventId, newGuest);
            return CreatedAtAction(nameof(GetGuestsForEvent), new { eventId = eventId }, createdGuest);
        }

        [HttpDelete("guests/{guestId}")]
        public async Task<IActionResult> DeleteGuest(int guestId)
        {
            await _eventService.DeleteGuest(guestId);
            return NoContent();
        }
       
        [HttpPost("{eventId}/register")]
        public async Task<IActionResult> RegisterForEvent(int eventId, [FromBody] RegistereventDto registerEventDto)
        {
            try
            {
                var user = await _eventService.GetUserByIdAsync(registerEventDto.UserId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var eventDto = await _eventService.RegisterForEventAsync(eventId, registerEventDto.UserId, registerEventDto);
                return Ok(eventDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetRegisteredEvents(int userId)
        {
            var eventDtos = await _eventService.GetRegisteredEventsAsync(userId);

            if (eventDtos == null || !eventDtos.Any())
            {
                return NotFound("No events found for this user.");
            }

            return Ok(eventDtos);
        }
        [HttpGet("admin/bookedEvents")]
        public IActionResult GetAllBookedEvents()
        {
            // Logic to fetch all booked events
            var bookedEvents = _eventService.GetAllBookedEvents();
            return Ok(bookedEvents);
        }


    }
}
