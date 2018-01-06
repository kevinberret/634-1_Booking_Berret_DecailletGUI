using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.Models
{
    public class Reservation
    {
        public int IdReservation { get; set; }
        public string Fisrtname { get; set; }
        public string Lastname { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public Hotel Hotel { get; set; }

        public override string ToString()
        {
            string str = "";
            str += "idReservation : " + IdReservation +
                "Hotel : " + Hotel.Name +
                " Room : ";
            foreach (Room r in Rooms)
            {
                str += r.Number + "/";
            }
            str += "Check in : " + CheckIn +
                "Check out : " + CheckOut;
            return str;
        }
    }
}