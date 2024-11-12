using BillFolio.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillFolio.ViewModel
{
    public class BillViewModel
    {
        // A collection of bills
        public List<Bill> Bills { get; set; } = new List<Bill>();

        public BillViewModel()
        {
            // Sample bills data (replace with actual data binding or service call)
            Bills = new List<Bill>
            {

            };
        }

        // This method will get bills for a specific date
        public List<Bill> GetBillsForDate(DateTime date)
        {
            return Bills.Where(bill => bill.DueDate.Date == date.Date).ToList();
        }
    }
}
