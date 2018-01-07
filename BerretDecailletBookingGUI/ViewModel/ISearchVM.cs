using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.ViewModel
{
    public abstract class ISearchVM
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}