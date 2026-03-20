using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DFWGeeksAndNerds.Models
{
    public class EventViewModel
    {
        public string VenueName { get; set; }
        public DateTime DateofEvent { get; set; }
        public double EventCost { get; set; }
        public string VenueDescription { get; set; }
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
    }
}
