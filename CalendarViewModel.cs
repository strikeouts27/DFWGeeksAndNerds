using System;

namespace DFWGeeksAndNerds.Models
{
    public class CalendarViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public DateTime CurrentDate => new DateTime(Year, Month, 1);
        public int DaysInMonth => DateTime.DaysInMonth(Year, Month);
        
        // This tells us what day of the week (0=Sunday, 6=Saturday) the 1st falls on
        public DayOfWeek FirstDayOfMonth => CurrentDate.DayOfWeek;
    }
}