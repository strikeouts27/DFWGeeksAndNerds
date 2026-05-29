namespace DFWGeeksAndNerds.Services; 
using DFWGeeksAndNerds.DTOs;
using Newtonsoft.Json;
/*
 For this project to work you must right click on the solutions file and selecting properties. You need to tell visual studio to launch both the main project and the api.  
 
 */
public class EventsDataService
{
    // a variable that stores this built in ASP.NET object of HttpClient for reference use because you can't just pull things out of thin air you have to specify parts of code sometimes. . 
    private readonly HttpClient _client; 
    // this is a targeting instructions for firing information to an api for CreateClient. 
    private const string clientName = "GeeksAndNerdsAPI";

    // a constructor was made to go get data. 
    // services empowers an object to speak with the api controller about getting data here. 
    // see above for object instantation and the materials needed for the create client function. 
    public EventsDataService(IHttpClientFactory factory)
    {
        // this creates a bridge  between the services and the api controller 
        // Create Client needs to know what string to use for the url. 
        // _client = new HttpClient (ClientName)
        // in this technique we create placeholder variables and than combine the parts togather to make a client. 
        // the purpose of a client is that it visit urls, sends http requests with the data that we hand it, and than it sends the user the response. 
        _client = factory.CreateClient(clientName);
    }
    // services acts as a bridge between the api and the controller. 
    
    public async Task<List<EventDTO>> GetEventsAsync()
    {
        // See the swagger get request executed. you should see a request url 
        // this code will say go into the request url and grab everything in the url that is before the slashes. 
        
        // using this connection point, go get me these events. it calls the events controller using the functonality of the getasync method. so see events controller.cs for the receiption point.
        // this line is calling code that we wrote in the api. 
        // this is where we are telling our empowered object to grab event data from the api controller.
        // going out to the api controller and waiting for information.
        // if the method is asking for information from something outside of the program that is a sign it is a get request. This projects program and API are in different programs. 
        // responses are not HTTP Request types responses are a type of package getting re
        var response = await _client.GetAsync("events");
        // this will ensure that the response is successful, if not it will throw an error. FAIL FAST 
        response.EnsureSuccessStatusCode();
        // this will take the response content and make it into a string. then it will deserialize the string into a list of EventDTO objects.
        // ASP.NET specifies that json will be the format that is transmitted.  
        // upon success the event information is translated into a string which is than stored in a format fitting for the next method to package it into a DTO. 
        
        var content = await response.Content.ReadAsStringAsync();
        // desearlize think convert from string to object. it will assign all of the proprties of json into the corresponding fields by name.
        // this will return a list of events. 

        // .JsonSerializer is microsofts json convertor built by microsoft. joseph did NOT reccomend using it. .JsonSerializer 
        //return System.Text.Json.JsonSerializer.Deserialize<List<EventDTO>>(content);
        // deserialize and pack to DTO
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<EventDTO>>(content);
    }

    // The controller called services with this converted dto. and the dto is passed in as a parameter. 
    public async Task CreateEventAsync(EventDTO newEventDTO)
    {
        var jeventDTO = JsonConvert.SerializeObject(newEventDTO);
        // client was created earlier in the program see the top of the code page. 
        // THIS METHOD IS HTTP POST TO THE URL OF THE API. HERES THE ADDRESS AND FIRE THE INFORMATION. 
        // If the transmission is succesful it will transmit 200 or a success code. If not, an error message will happen. 
        var response = await _client.PostAsync("events", new StringContent(jeventDTO, System.Text.Encoding.UTF8, "application/json"));
        // this is the checker that will see if the transmission was succesful. if not, an error exception will display. 
        response.EnsureSuccessStatusCode();

        // there is no return keyword here but there is an implicit return. 
    }
}