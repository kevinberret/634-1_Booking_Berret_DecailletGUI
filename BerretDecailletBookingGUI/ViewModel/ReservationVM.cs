using BerretDecailletBookingGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BerretDecailletBookingGUI.ViewModel
{
    public class ReservationVM
    {
        public List<Room> Rooms { get; set; }
        public Reservation Reservation { get; set; }
    }
}