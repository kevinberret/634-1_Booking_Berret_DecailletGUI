using BerretDecailletBookingGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BerretDecailletBookingGUI.ViewModel
{
    public class RoomSelectedVM
    {
        public Room Room { get; set; }
        public bool IsSelected { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}