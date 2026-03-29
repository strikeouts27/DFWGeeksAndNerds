using System.ComponentModel.DataAnnotations;
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
        public string TechRequirements { get; set; }
        [Display(Name = "Is this event kid-friendly?")]

        // KID FRIENDLY
        public bool IsKidFriendly { get; set; }
        [DataType(DataType.MultilineText)]
        public string VenueDescription { get; set; }
        public string NewEvent { get; set; }
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

    }
}
