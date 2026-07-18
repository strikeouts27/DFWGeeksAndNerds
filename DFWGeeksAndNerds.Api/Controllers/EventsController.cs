// Events Controller.cs 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DFWGeeksAndNerds.Api.Models;
using System.ComponentModel;
using DFWGeeksAndNerds.Api.Providers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// DATABASE NEEDS TO BE TURNED ON IN ORDER TO RUN ANY OF THESE METHODS. 
// TO RUN THIS PROGRAM USE http://localhost:8000/swagger/index.html 
// use the dotnet build and dotnet run commands in the terminal to run the program. 


namespace DFWGeeksAndNerds.Api.Controllers
{
    // This Route attribute tag will parse the url and remove the word controller and than whatever is left of the name of the controller is how the controller will be accessed. The controllers own url so to speak. 
    [Route("[controller]")]
    // This next attribute specifies that this controller will be used for api calls.
    [ApiController]
    public class EventsController : ControllerBase
    {
        // static List<EventDTO> events = new List<EventDTO>
        // {
        //     When a property is marked as required, it must be initialized in the object initializer... SO I have to make a field for it. 

        //     new EventDTO
        //     {
        //         Id = 1,
        //         EventName = "Board Game Night",
        //         EventHost = "Andrew",
        //         RSVPCount = 10,
        //         VenueName = "Local Library",
        //         EventDate = DateTime.Now.AddDays(7),
        //         IsKidFriendly = true,
        //     }, 
        //     new EventDTO
        //     { 
        //         Id = 2, 
        //         EventName = "Bobs Game Night",
        //         EventHost = "Bob",
        //         RSVPCount=10,
        //         VenueName = "Bob's House",
        //         EventDate= DateTime.Now.AddDays(7),
        //         IsKidFriendly = true,
        //     },

        //     new EventDTO
        //     {
        //         Id = 3,
        //         EventName = "Anime Watch Party",
        //         EventHost = "Jason",
        //         RSVPCount= 8,
        //         VenueName = "Jasons House",
        //         EventDate= DateTime.Now.AddDays(7),
        //         IsKidFriendly = true,
        //     },
        //     new EventDTO
        //     {
        //         Id = 4,
        //         EventName = "Airsoft",
        //         EventHost = "Matt",
        //         RSVPCount=20,
        //         VenueName = "AirsoftVenue",
        //         EventDate= DateTime.Now.AddDays(7),
        //         IsKidFriendly = true,
        //     },

        //     new EventDTO
        //     {
        //         Id = 5,
        //         EventName = "Grand Theft Auto Tournament",
        //         EventHost = "Doug",
        //         RSVPCount=15,
        //         VenueName = "Doug House",
        //         EventDate= DateTime.Now.AddDays(7),
        //         IsKidFriendly = false,
        //     },
        // };

        // private object games;
        private readonly NerdsandGeeksDbContext _dbContext;

        // dependency injection is told to inject the NerdsandGeeksDbContext into the constructor of the EventsController. This allows the controller to use the database context for data operations, such as querying or saving data related to events.
        // This injected db context is only available in the scope of the event controller constructor method. 
        // Constructor 
        public EventsController(NerdsandGeeksDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        // GET: api/<EventsController>
        // IEnumerable is a collection of storage containers.
        // interface (contract of standards) it represents types of collections. 
        // You might say this is our way of doing a search query in a database. 
        [HttpGet]
        public IEnumerable<EventDTO> Get()
        {
            // the api controller grabbing the data from the database to be sent back via json 
            return _dbContext.Events.ToList();
        }

        [HttpGet("names")]
        public IEnumerable<EventNameDTO> GetNames()
        {
            // the api controller grabbing the data from the database to be sent back via json 
            return _dbContext.Events
                             .Select(p => new EventNameDTO { Id = p.Id, EventName = p.EventName, EventDate = p.EventDate })
                             .ToList();
        }

        // POST api/<EventsController>
        // event is a special keyword for dotnet. @ says this is not a dotnet keyword. 
        // newEvent is the name of the variable that will store this Event message data. 
        // The model that we made for Event is now the datatype of the storage container. 
        // FromBody is telling the API method to grab the data from the api body of the api message that is being sent. 
        // FromBody is a declaration that this post method meets these standards a FromBody attribute must have. 
        // THE DATABASE NEEDS TO BE TURNED ON IN ORDER TO RUN ANY OF THESE METHODS. 
        [HttpPost]
        public async Task Post([FromBody] EventDTO newEvent)
        {
            // this is where we would add the new event to the database. 
            // we would use the db context to add the new event to the database. 
            // know that there one place is a db context resource that is availabe via dependency injection that is capable of storing and managing data to a database. 
            // know which table in the database I want to save the data to. 
            _dbContext.Events.Add(newEvent);
            // We create the operation and it runs completely and successfully before we save any changes to the database. We save changes on post, update, and delete commands. Get is just for retrieving data so we don't need to save changes.
            _dbContext.SaveChanges();
        }


        [HttpPut("id")]
        // Task after the method name is similar to the void keybord. 
        // if there are angle brackets <> on the Task keyword would return something. If no brackets there is no return. (tasks are specalized and will be addressed later.) pending more details
        // angle brackets <> are return types. 
        // Tasks are data types that work with async programming. async functions primarly deal with moving async functions forward. in other words multi tasking. 
        // async -> doing multiple things at the same time.  
        // await you can do this other stuff around me.

        // the naming convention of the parameter is important it needs to be a verb that describes what the method is targeting and updating.
        // DTO or POCO only contians data. 
        // FirstOrDefault is a method that will return the first item in the collection that matches the condition. If no items match the condition, it will return the default value for the type (null for reference types). If it is null we won't make any changes to the database the record does not exisit. 
        // DATABASE NEEDS TO BE TURNED ON IN ORDER TO RUN ANY OF THESE METHODS. 
        // streaming is when information is coming in from a user or information source and streaming is a container that holds the information and upon completetion of recieving all incoming data will transmit everything it recorded in the exact order it got it. 
        // FromBody is a shortcut for taking the package, reading the body and putting everything into a variable. It is a packaging like function that grabs the content, takes in any stream of informaiton and than deserialize the content and than stores that compelted work as a variable. 
        // we know that put request is targeting information. 
        // get the content, stream it, and serialize it, and put it into a variable. 
        public async Task Update(int id, [FromBody] EventDTO updatedEvent)
        {
            var eventToUpdate = _dbContext.Events.FirstOrDefault(p => p.Id.Equals(id));
            if (eventToUpdate == null) 
                return;

            // target all information that you want to update and overwrite it with the new information that is being sent in the api message.
            // keep in mind that this information is stored in columns. 
            // we are specifying what the put request will target once it finds the right record.
            // it may be best practice to have the database pulled up so you can see what columns exist.
            // keep in mind that if you create a new column in the database you have to come to asp.net and say so for all methods involving the new column. 
            // checklist dto, view and the testing phase. 

            eventToUpdate.EventName = updatedEvent.EventName;
            eventToUpdate.EventHost = updatedEvent.EventHost;
            eventToUpdate.GuestCount = updatedEvent.GuestCount;
            eventToUpdate.VenueName = updatedEvent.VenueName;
            eventToUpdate.EventDate = updatedEvent.EventDate;
            eventToUpdate.IsKidFriendly = updatedEvent.IsKidFriendly;
            eventToUpdate.Description = updatedEvent.Description;
            _dbContext.Events.Update(eventToUpdate);
            _dbContext.SaveChanges();
            return; // no information is returned. is implicitly returnted but we added return; to explicitly say it. 

        
        }
        // alternative to from body 
        // (int id, HttpRequest request) 
        // var updatedEvent = Deserialize(InputFormatter(response.content.body)); 

        // DELETE api/<EventsController>/5
        // DATABASE NEEDS TO BE TURNED ON IN ORDER TO RUN ANY OF THESE METHODS. 
        [HttpDelete]
        public async Task Delete(int id)
        {
            var eventToDelete = _dbContext.Events.FirstOrDefault(p => p.Id.Equals(id));
            if (eventToDelete == null)
                return;
            _dbContext.Events.Remove(eventToDelete);
            _dbContext.SaveChanges();
        }
    }
}
