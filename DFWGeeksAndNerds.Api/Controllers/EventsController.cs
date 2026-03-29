using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DFWGeeksAndNerds.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DFWGeeksAndNerds.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        static List<Event> events = new List<Event>
        {
            // When a property is marked as required, it must be initialized in the object initializer... SO I have to make a field for it. 

            new Event
            {
                Id = 1,
                EventName = "Board Game Night",
                EventHost = "Andrew",
                RSVPCount = 10,
                VenueName = "Local Library",
                EventDate = DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            }, 
            new Event
            { 
                Id = 2, 
                EventName = "Bobs Game Night",
                EventHost = "Bob",
                RSVPCount=10,
                VenueName = "Bob's House",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            },

            new Event
            {
                Id = 3,
                EventName = "Anime Watch Party",
                EventHost = "Jason",
                RSVPCount= 8,
                VenueName = "Jasons House",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            },
            new Event
            {
                Id = 4,
                EventName = "Airsoft",
                EventHost = "Matt",
                RSVPCount=20,
                VenueName = "AirsoftVenue",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            },

            new Event
            {
                Id = 5,
                EventName = "Grand Theft Auto Tournament",
                EventHost = "Doug",
                RSVPCount=15,
                VenueName = "Doug House",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = false,
            },
        };
        private object games;

        // GET: api/<EventsController>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return events;
        }

        // POST api/<EventsController>
        [HttpPost]
        public async Task Post([FromBody] Event @event)
        {
            events.Add(@event); 
        }

        [HttpPut("id")]
        public async Task Update(int id, [FromBody] EventDTO @event)
        {
            var eventToUpdate = events.FirstOrDefault(p => p.Id.Equals(id));
            if (eventToUpdate == null) 
                return;

            eventToUpdate.EventName = @event.EventName;
            eventToUpdate.EventHost = @event.EventHost;
            eventToUpdate.RSVPCount = @event.RSVPCount;
            eventToUpdate.VenueName = @event.VenueName;
            eventToUpdate.EventDate = @event.EventDate;
            eventToUpdate.IsKidFriendly = @event.IsKidFriendly;
            eventToUpdate.Description = @event.Description;
            events.RemoveAll(p => p.Id.Equals(id));
            events.Add(eventToUpdate);
        
        }

        // DELETE api/<EventsController>/5
        [HttpDelete]
        public async Task Delete(int id)
        {
            events.RemoveAll(p => p.Id.Equals(id));
        }
    }
}
