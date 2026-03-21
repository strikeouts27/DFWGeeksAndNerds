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
        [Range(1, 1000, ErrorMessage = "Guest count must be between 1 and 1000")]
        [Display(Name = "Estimated Guests")]
        public int GuestCount { get; set; }
        [Required]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Budget")]
        public double EventCost { get; set; }
        public string TechRequirements { get; set; }
        [Display(Name = "Is this event kid-friendly?")]
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
