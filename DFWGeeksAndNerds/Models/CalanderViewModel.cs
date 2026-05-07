using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Collections.ObjectModel;

namespace DFWGeeksAndNerds.Models
{
    public class CalanderViewModel
    {
        // Calander Requirements
        // We don't want outside influence on what can control the month and year. 
        public int Month { get; private set; }
        
        public int Year { get; private set; }

        public string DisplayMonth
        {
            get {
                return new DateTime(Year, Month, 1).ToString("MMMM");
            }
        }

        public string DisplayYear => Year.ToString();

        // The why behind this line of code is that we want an event container that is read only
        // and the reason why is because we are only displaying events on the events page. 
        public ReadOnlyCollection<EventViewModel> Events { get; set; }

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
    }
}
