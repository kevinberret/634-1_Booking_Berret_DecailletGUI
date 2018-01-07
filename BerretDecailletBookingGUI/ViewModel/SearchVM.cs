using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.ViewModel
{
    public class SearchVM : ISearchVM
    {
        public String Location { get; set; }

        public String toString() {
            return Location + "/"
                + CheckIn.ToString("yyyy-MM-dd") + "/"
                + CheckOut.ToString("yyyy-MM-dd");
        }
    }
}