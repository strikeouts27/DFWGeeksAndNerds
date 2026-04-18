using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Api.Models;

namespace DFWGeeksAndNerds.Api.Controllers
{
    // we can use the comment here and make the route more specific for api's and we can also include a version number for different versions. (optional)
    // THe comment showed it how it was before Joseph changed it. 
    //[Route("api/version1/[controller]")]
    [Route("[controller]")] // this will take the name of the controller and use it as the route. so in this case it will be /games
    [ApiController]
    public class GamesController : ControllerBase
    {
        // static List<Game> games = new List<Game>
        // {
        //     new Game { Id = 1, Name = "Game 1" },
        //     new Game { Id = 2, Name = "Game 2" },
        //     new Game { Id = 3, Name = "Game 3" },
        //     new Game { Id = 4, Name = "Game 4" },
        //     new Game { Id = 5, Name = "Game 5" },
        // };

        // make a variable to hold the database context and provide a label for it via variable name. 
        private readonly NerdsandGeeksDbContext _dbContext; 
        
        // create a constructor for the db context. 
        public GamesController(NerdsandGeeksDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        // write the [HttpGet] method and have it target the correc table. 
        [HttpGet]
        public IEnumerable<Game> Get()

        
        {
            // this is a blueprint for a json message. 

            // the select function acts like a projecter. that projectotion can be molded to what you want it to be. 
            // the projector will be given numbers between 1-5 while that is happening operate this lambda function on this iteartion. 
            // make a range of numbers from one to five. as you iterate, showcase this index and make a game object in that enumerable. 
            // once you do do the logic inside of the curly brances for each item of the range and than put that information in an array.

            //var games = Enumerable.Range(1, 5).Select(index => new Game
            //{
            //    Id = index, 
            //    Name = $"Game {index}",
            //})
            //.ToArray();
            
            // microsoft has a built in json serializer that will automatically serialize the object to json when we return it from the controller.
            // so we don't have to do a conversion unless we change the default settings so much that we have to do it ourselves.
            return _dbcontext.Games.ToList();    
        }

        [HttpPost]
        public async Task Post([FromBody] GameDTO game) 
        {
            _dbContext.Games.Add(game);
            _dbContext.SaveChanges();
        }

        // patch update or insert if it doesn't exisit insert it, if it does exist update. 

        [HttpPut("id")]
        public async Task Update(int id,[FromBody] GameDTO game)
        {
            // this will search for the game with an id that we want to update. if if it does't find anything it will return null.'
            var gameToUpdate = _dbContext.Games.FirstOrDefault(p => p.Id.Equals(id));
            if (gameToUpdate == null)
                return;

            // if the search does find something, it will update the information to what is targeted by the code below. \
            // it is possble to write code in which it finds nothing and will create a new entry in the datbase.
            // id's should not be updated. leave the id field alone. 
            // update the attributes w eneed to update . see your corresponding models file. 
                gameToUpdate.Name = game.Name;
                gameToUpdate.Description = game.Description;
                
                // this commented method was used for mock data. it was designed to clean up the entries we made when we were done testing. 
                // games.RemoveAll(p => p.Id.Equals(id));

                // this command was for mock data setup. 
                // games.Add(gameToUpdate);

                _dbContext.SaveChanges(); 
                
            }

        [HttpDelete]
        public async Task Delete(int id)
        {
            var gameToDelete = _dbContext.Games.FirstOrDefault(p => p.Id.Equals(id)); 
            if (eventToDelete == null)
                return; 
            
            _dbContext.Events.Remove(eventToDelete); 
            _dbContext.SaveChanges(); 

            // we used this commented method below during the mock data stage. 
            // games.RemoveAll(p => p.Id.Equals(id));
        }

    }
}
