using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.Models
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Category { get; set; }
        public bool? Wifi { get; set; }
        public bool? Parking { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public ICollection<Room> rooms { get; set; }

        public override string ToString()
        {
            return "idHotel : " + IdHotel +
                " description : " + Description +
                " location : " + Location +
                " category : " + Category +
                " has wifi : " + Wifi +
                " has parking : " + Parking +
                " phone: " + Phone +
                " email: " + Email +
                " website : " + WebSite;
        }
    }
}