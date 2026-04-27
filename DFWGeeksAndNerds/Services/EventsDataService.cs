namespace DFWGeeksAndNerds.Services; 
using DFWGeeksAndNerds.DTOs;
public class EventsDataService
{
    // THis was created in Program.cs 
    private readonly HttpClient _client; 
    private const string clientName = "GeeksAndNerdsAPI";

    public EventsDataService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(clientName);
    }

    public async Task<List<EventDTO>> GetEventsAsync()
    {
        // See the swagger get request executed. you should see a request url 
        // this code will say go into the request url and grab everything in the url that is before the slashes. 
        // example https://localhost:5003/events this would grab https://localhost:5003/ and then add the "events" to the end of it.
        var response = await _client.GetAsync("events");
        // this will ensure that the response is successful, if not it will throw an error. FAIL FAST 
        response.EnsureSuccessStatusCode();
        // this will take the response content and make it into a string. then it will deserialize the string into a list of EventDTO objects.
        var content = await response.Content.ReadAsStringAsync();
        // desearlize think convert from string to object. it will assign all of the proprties of json into the corresponding fields by name.
        // this will return a list of events. 

        // .JsonSerializer is microsofts json convertor built by microsoft. joseph did NOT reccomend using it. .JsonSerializer 
        //return System.Text.Json.JsonSerializer.Deserialize<List<EventDTO>>(content);

        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<EventDTO>>(content);
    }
}