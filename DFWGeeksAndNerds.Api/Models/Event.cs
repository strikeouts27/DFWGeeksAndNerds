namespace DFWGeeksAndNerds.Api.Models
{
    public class Event 
    {
        public int Id { get; set; } 
        public required string EventName { get; set; }
        public required string EventHost { get; set; }
        public required int GuestCount { get; set; }
        public required string VenueName { get; set; }
        public required DateTime EventDate { get; set; }
        public required bool IsKidFriendly { get; set; }
        public string? Description { get; set; }
    }

    public class EventDTO
    {
        public int Id { get; set; }
        public required string EventName { get; set; }
        public required string EventHost { get; set; }
        public required int GuestCount { get; set; }
        public required string VenueName { get; set; }
        public required DateTime EventDate { get; set; }
        public required bool IsKidFriendly { get; set; }
        public string? Description { get; set; }
    }

}
