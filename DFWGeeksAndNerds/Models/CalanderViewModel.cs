using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace DFWGeeksAndNerds.Models
{
    public class CalanderViewModel
    {
        // Calander Requirements
        // We don't want outside influence on what can control the month and year. 
        // we had private on the set methods to control outside influence but the json deserailizer that we were using cannot utilize private fields. 
        public int Month { get; set; }
        
        public int Year { get; set; }

        // The why behind this line of code is that we want an event container that is read only
        // and the reason why is because we are only displaying events on the events page. 
        public List<EventViewModel> Events { get; set; }

        public ReadOnlyCollection<EventViewModel> CurrentEvents(int currentDay) => 
            Events.Where(e => e.DateofEvent.Day == currentDay && e.DateofEvent.Month == Month && e.DateofEvent.Year==Year)
                  .ToList()
                  .AsReadOnly();

        public string DisplayMonth
        {
            get {
                return new DateTime(Year, Month, 1).ToString("MMMM");
            }
        }

        public string DisplayYear => Year.ToString();


        public async Task MoveNext ()
        {
            if (Month == 12)
            {
                Month = 1;
                Year++;
            }
            else
            {
                Month++;
            }
        }

        public async Task MovePrevious ()
        {
            if (Month == 1)
            {
                Month = 12;
                Year--; 
            }
            else
            {
                Month--; 
            }
        }

        // constructors MUST have the same names as its class. 
        // initalize to todays month and year by default. 
        public CalanderViewModel()
        {
            Month = DateTime.Today.Month;
            Year = DateTime.Today.Year; 
        }

        // take in a month and year so we can set it. 
        public CalanderViewModel(int year, int month) {

            Month = month;
            Year = year; 
        }
    }
}
