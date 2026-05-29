using System.ComponentModel.DataAnnotations;
using DFWGeeksAndNerds.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DFWGeeksAndNerds.Models
{
    public class EventViewModel
    {

        /*
        NOTES: 

        */
        [Required(ErrorMessage = "Host name is required")]
        [Display(Name = "Event Host")]
        public string EventHost { get; set; }

        // EVENT NAME 
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }

        // This will show a truncated event name for the calander. 
        public string DisplayName => EventName.Substring(0, EventName.Length > 30 ? 30 : EventName.Length);
        // GUEST COUNT 
        public int GuestCount { get; set; }
        [Required]
        [Display(Name = "Venue Name")]

        // VENUE NAME 
        public string VenueName { get; set; }

        // EVENT COST
        [DataType(DataType.Currency)]
        [Display(Name = "Total Budget")]
        public double EventCost { get; set; }

        // TECH REQUIREMENTS
        public string? TechRequirements { get; set; }
        [Display(Name = "Is this event kid-friendly?")]

        // KID FRIENDLY
        public bool IsKidFriendly { get; set; }
        [DataType(DataType.MultilineText)]
        public string? VenueDescription { get; set; }
        
        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime DateofEvent { get; set; }

        /*
        public DateTime now = new DateTime();  // Current date with the time set to midnight (00:00:00)
        public string[] days { get; set; } =
        {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday", 
            "Friday",
            "Saturday"
        };

        //public void OnGet()
        //{
        //    days = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        //}
        //public void OnPost()
        //{
        //    days = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        //}
        */
        // the internal keyword is used to restrict access to the method within the same project
        // this is the standard way of dealing with a basic dto. 

        internal static EventDTO ConvertToEventDTO(EventViewModel model)
        {
            return new EventDTO
            {
                // the left side of the equals sign is related to the properties of the event view model. 
                // the right side of the equals sign is related to the properties of the event DTO.
                EventHost = model.EventHost,
                EventName = model.EventName,
                GuestCount = model.GuestCount,
                VenueName = model.VenueName,
                EventDate = model.DateofEvent,
                IsKidFriendly = model.IsKidFriendly,
                Description = model.VenueDescription, 
                // if the data types don't line up use the parse method to convert it to the rigth data type.
                // DateofEvent = DateTime.Parse(dto.EventDate),
            };
        }

        internal static EventViewModel ConvertToViewModel(EventDTO dto)
        {
            return new EventViewModel
            {
                // the left side of the equals sign is related to the properties of the event view model. 
                // the right side of the equals sign is related to the properties of the event DTO.
                EventHost = dto.EventHost,
                EventName = dto.EventName,
                GuestCount = dto.GuestCount,
                VenueName = dto.VenueName,
                DateofEvent = dto.EventDate,
                IsKidFriendly = dto.IsKidFriendly,
                VenueDescription = dto.Description,
                // if the data types don't line up use the parse method to convert it to the rigth data type.
                // DateofEvent = DateTime.Parse(dto.EventDate),
            };
        }

        // this method loops through the dtos sent to it and converts each one to a view model using the ConvertToViewModel method. it then adds each view model to a list and returns the list of view models.
        // we don't want outsiders aware of methods and other information. just the data objects. 
        // dto is to send data in and out. 
        // view model is for functionality for data, present, calculate etc. 
        // this method is for handling dto's that were transferred as lists. 
        internal static List<EventViewModel> ConvertToViewModelList(List<EventDTO> dtos)
        {
            var viewModels = new List<EventViewModel>();
            foreach (var dto in dtos)
            {
                viewModels.Add(ConvertToViewModel(dto));
            }
            return viewModels;
        }
    }
}
