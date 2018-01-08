using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.ViewModel
{
    public class SearchVM : ISearchVM
    {
        public String Location { get; set; }
        public List<String> Locations { get; set; }

        public SearchVM()
        {
            CheckIn = DateTime.Today;
            CheckOut = CheckIn.Add(new TimeSpan(1, 0, 0, 0));
        }

        public String toString() {
            return Location + "/"
                + CheckIn.ToString("yyyy-MM-dd") + "/"
                + CheckOut.ToString("yyyy-MM-dd");
        }
    }
}