using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Api.Controllers
{
    [ApiController]
    // Route tag specifies the naming conventions for your api to define which controller maps to the named endpoint.
    // The API is defined by endpoints, endpoints are defined by routing mechanisms, the routing mechanisms are defined by route the tag that the programmer specifices, which CAN BE defined by the controller class name.
    // in this case it takes the name of the class and chops off the controller word leaving /WeatherForecast
    [Route("[controller]")]
    // This endpoint would be named WeatherForecast MVC removes Controllerkeyword when you must referendce it. 
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        // this attribute tag will say this method can only be used in a get method. 
        // THere are a series of keywords or commands that we can make methods to trigger a response for. GET, POST, PUT, DELETE, PATCH, HEAD, OPTIONS, TRACE, CONNECT.       
        // in this case this would be for a get method. 

        //IENUMERABLE works with Lists Collections of data, Arrays. 
        // Invariance rules -> we cannot have something derivied from something that does not fit its type. apples fit in fruit collections, not potatoes. 
        
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            return weather; 
        }
    }
}
