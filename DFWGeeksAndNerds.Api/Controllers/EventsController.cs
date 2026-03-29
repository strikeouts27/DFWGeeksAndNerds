using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DFWGeeksAndNerds.Api.Models;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DFWGeeksAndNerds.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        static List<EventDTO> events = new List<EventDTO>
        {
            // When a property is marked as required, it must be initialized in the object initializer... SO I have to make a field for it. 

            new EventDTO
            {
                Id = 1,
                EventName = "Board Game Night",
                EventHost = "Andrew",
                RSVPCount = 10,
                VenueName = "Local Library",
                EventDate = DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            }, 
            new EventDTO
            { 
                Id = 2, 
                EventName = "Bobs Game Night",
                EventHost = "Bob",
                RSVPCount=10,
                VenueName = "Bob's House",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            },

            new EventDTO
            {
                Id = 3,
                EventName = "Anime Watch Party",
                EventHost = "Jason",
                RSVPCount= 8,
                VenueName = "Jasons House",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            },
            new EventDTO
            {
                Id = 4,
                EventName = "Airsoft",
                EventHost = "Matt",
                RSVPCount=20,
                VenueName = "AirsoftVenue",
                EventDate= DateTime.Now.AddDays(7),
                IsKidFriendly = true,
            },

            new EventDTO
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
        // IEnumerable is a collection of storage containers.
        // interface (contract of standards) it represents types of collections. 
        [HttpGet]
        public IEnumerable<EventDTO> Get()
        {
            return events;
        }

        // POST api/<EventsController>
        // event is a special keyword for dotnet. @ says this is not a dotnet keyword. 
        // newEvent is the name of the variable that will store this Event message data. 
        // The model that we made for Event is now the datatype of the storage container. 
        // FromBody is telling the API method to grab the data from the api body of the api message that is being sent. 
        [HttpPost]
        public async Task Post([FromBody] EventDTO newEvent)
        {
            events.Add(newEvent); 
        }

        [HttpPut("id")]
        // Task after the method name is similar to the void keybord. 
        // if there is brackets <> on the Task keyword would return something. If no brackets there is no return. (tasks are specalized and will be addressed later.) pending more details
        // the naming convention of the parameter is important it needs to be a verb that describes what the method is targeting and updating.
        // DTO or POCO only contians data. no functions. 
        public async Task Update(int id, [FromBody] EventDTO updatedEvent)
        {
            var eventToUpdate = events.FirstOrDefault(p => p.Id.Equals(id));
            if (eventToUpdate == null) 
                return;

            eventToUpdate.EventName = updatedEvent.EventName;
            eventToUpdate.EventHost = updatedEvent.EventHost;
            eventToUpdate.RSVPCount = updatedEvent.RSVPCount;
            eventToUpdate.VenueName = updatedEvent.VenueName;
            eventToUpdate.EventDate = updatedEvent.EventDate;
            eventToUpdate.IsKidFriendly = updatedEvent.IsKidFriendly;
            eventToUpdate.Description = updatedEvent.Description;
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
