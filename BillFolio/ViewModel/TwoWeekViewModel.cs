using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BillFolio.ViewModel
{
    internal class TwoWeekViewModel
    {
            // Johnathon's Additions 11/11/24
        public class CalendarDateWithBills
        {
            public DateTime Date { get; set; }
            public List<Bill> Bills { get; set; }
        }

        public class Bill
        {
            public string BillName { get; set; }
            // Add other properties related to the bill
        }

        public class CalendarViewModel
        {
            public List<CalendarDateWithBills> CalendarDates { get; set; }
        }

            // Johnathon's Additions 11/11/24 END
    }
}
