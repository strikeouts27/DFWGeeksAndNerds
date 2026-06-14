using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DFWGeeksAndNerds.Shared.DTOs
{
    public class EventDTO
    {
        // we can map what we expect from json to our properties using the JsonPropertyName attribute. this is useful when the json property name does not match our c# property name.
        // we can route data with different labels to the containers we want them to be. 
        //[JsonPropertyName("id")]
        // public int EID {get; set; } can be converted to id with the [] tags. 

        public int ID { get; set; }
        public string EventName { get; set; }
        public string EventHost { get; set; }
        [JsonPropertyName("rsvpCount")]
        public int GuestCount { get; set; }
        public string VenueName { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsKidFriendly { get; set; }
        public string Description { get; set; }
    }
}
