using EventManagment.context;
using EventManagment.Dtos;
using EventManagment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EventManagment.Services
{
    //public class EventService
    //{
    //}
    public interface IEventService
    {
        //Task<IEnumerable<Event>> GetEvents(EventDto EventDto);
        Task<IEnumerable<EventDto>> GetEvents();
        Task<Event> GetEventById(int eventId);
        Task<Event> CreateEvent(Event newEvent);
        Task<Event> CreateEventWithBudget(Event newEvent);
        Task<Event> UpdateEvent(Event updatedEvent);
        IEnumerable<Event> GetAllBookedEvents();

        Task DeleteEventAsync(int eventId);
        //Guest-related methods
        Task<IEnumerable<Guest>> GetGuestsForEvent(int eventId);
        Task<Guest> AddGuestToEvent(int eventId, Guest newGuest);
        Task DeleteGuest(int guestId);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<EventDto>> GetRegisteredEventsAsync(int userId);
        Task<EventDto> RegisterForEventAsync(int eventId, int userId, RegistereventDto registerEventDto);
     
    }
    public class EventService : IEventService
    {
        private readonly Context _context;

        public EventService(Context context)
        {
            _context = context;
        }
        public IEnumerable<Event> GetAllBookedEvents()
        {
            return _context.Events
                           .Include(e => e.Guests)       // Included related guests
                           .Include(e => e.Budget)     // Included related budget if any
                           .Where(e => e.Guests.Any())
                           .ToList();
        }
        public async Task<IEnumerable<EventDto>> GetEvents()
        {
            return await _context.Events
                .Include(e => e.User)
                .Include(e => e.Budget)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    EventName = e.EventName,
                    Date = e.Date,
                    Time = e.Time,
                    Location = e.Location,
                    Description = e.Description,
                    UserId = e.UserId,
                    Budget = e.Budget == null ? null : new BudgetDto
                   // Budget = new BudgetDto
                    {
                        Expenses = e.Budget.Expenses,
                        Revenue = e.Budget.Revenue
                    }
                })
                .ToListAsync();
        }

        public async Task<Event> GetEventById(int eventId)
        {
            return await _context.Events.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == eventId);
        }
 

        public async Task<Event> CreateEvent(Event newEvent)
        {
            var user = await _context.Users.FindAsync(newEvent.UserId);
            if (user == null)
                throw new Exception($"User with ID {newEvent.UserId} does not exist.");

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event> CreateEventWithBudget(Event newEvent)
        {
            var user = await _context.Users.FindAsync(newEvent.UserId);
            if (user == null)
                throw new Exception($"User with ID {newEvent.UserId} does not exist.");

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            if (newEvent.Budget != null)
            {
                newEvent.Budget.EventId = newEvent.Id;
                _context.Budgets.Add(newEvent.Budget);
                //  await _context.SaveChangesAsync();
            }

            return newEvent;
        }

        public async Task<Event> UpdateEvent(Event updatedEvent)
        {
            var existingEvent = await _context.Events.Include(e => e.Budget).FirstOrDefaultAsync(e => e.Id == updatedEvent.Id);
            if (existingEvent == null)
                throw new Exception($"Event with ID {updatedEvent.Id} does not exist.");

            existingEvent.EventName = updatedEvent.EventName;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.Time = updatedEvent.Time;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.UserId = updatedEvent.UserId;

            if (existingEvent.Budget == null)
                existingEvent.Budget = new Budget();

            existingEvent.Budget.Expenses = updatedEvent.Budget.Expenses;
            existingEvent.Budget.Revenue = updatedEvent.Budget.Revenue;

            await _context.SaveChangesAsync();

            return existingEvent;
        }

      
        public async Task DeleteEventAsync(int eventId)
        {
            var eventToDelete = await _context.Events.Include(e => e.Budget)
                                                     .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventToDelete != null)
            {
                // Remove related budgets first
                _context.Budgets.RemoveRange(eventToDelete.Budget);

                // Remove the event
                _context.Events.Remove(eventToDelete);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Guest>> GetGuestsForEvent(int eventId)
        {
            return await _context.Guests.Where(g => g.EventId == eventId).ToListAsync();
        }

        public async Task<Guest> AddGuestToEvent(int eventId, Guest newGuest)
        {
            newGuest.EventId = eventId;
            _context.Guests.Add(newGuest);
            await _context.SaveChangesAsync();
            return newGuest;
        }

        public async Task DeleteGuest(int guestId)
        {
            var guestToDelete = await _context.Guests.FindAsync(guestId);
            if (guestToDelete != null)
            {
                _context.Guests.Remove(guestToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<EventDto> RegisterForEventAsync(int eventId, int userId, RegistereventDto registerEventDto)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Guests)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
            {
                throw new Exception("Event not found.");
            }

            // Check if user is already registered
            var isUserRegistered = eventEntity.Guests.Any(g => g.UserId == userId);
            if (isUserRegistered)
            {
                throw new Exception("User already registered for this event.");
            }
            var user = await _context.Users.FindAsync(userId);
            var guest = new Guest
            {
                UserId = userId,
                //UserName = registerEventDto.UserName,
                //PhoneNumber = registerEventDto.MobileNumber,
                PhoneNumber = user.MobileNumber,
                UserName=user.userName,
               
                EventId = eventId
            };

            eventEntity.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return new EventDto
            {
                Id = eventEntity.Id,
                EventName = eventEntity.EventName,
                Date = eventEntity.Date,
                Time = eventEntity.Time,
                Location = eventEntity.Location,
                Description = eventEntity.Description,
                Guests = eventEntity.Guests.Select(g => new GuestDto
                {
                    Id = g.Id,
                    UserName = g.UserName,
                    PhoneNumber = g.PhoneNumber
                }).ToList(),
                Budget = new BudgetDto
                {
                    Expenses = eventEntity.Budget?.Expenses ?? 0,
                    Revenue = eventEntity.Budget?.Revenue ?? 0
                }
            };
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task<IEnumerable<EventDto>> GetRegisteredEventsAsync(int userId)
        {
            // Fetch the events where the user is registered
            var events = await _context.Events
                .Where(e => e.Guests.Any(g => g.UserId == userId))
                .Include(e => e.Guests) // Ensure guests are included in the response
                .ToListAsync();

            if (events == null || !events.Any())
            {
                return new List<EventDto>(); // Return an empty list if no events are found
            }

            // Convert to DTOs
            var eventDtos = events.Select(e => new EventDto
            {
                Id = e.Id,
                EventName = e.EventName,
                Date = e.Date,
                Time = e.Time,
                Location = e.Location,
                Description = e.Description,
                Budget = e.Budget != null ? new BudgetDto
                {
                    Id = e.Budget.Id,
                    Expenses = e.Budget.Expenses,
                    Revenue = e.Budget.Revenue
                } : null, // Set Budget to null if e.Budget is null
                Guests = e.Guests != null ? e.Guests.Select(g => new GuestDto
                {
                    Id = g.Id,
                    UserName = g.UserName,
                    PhoneNumber = g.PhoneNumber
                }).ToList() : new List<GuestDto>() // Return an empty list if e.Guests is null
            }).ToList();
            //    Budget = new BudgetDto
            //    {
            //        Id = e.Budget.Id,
            //        Expenses = e.Budget.Expenses,
            //        Revenue = e.Budget.Revenue
            //    },
            //    Guests = e.Guests.Select(g => new GuestDto
            //    {
            //        Id = g.Id,
            //        UserName = g.UserName,
            //        PhoneNumber = g.PhoneNumber
            //    }).ToList()
            //}).ToList();

            return eventDtos;
        }
    }
}