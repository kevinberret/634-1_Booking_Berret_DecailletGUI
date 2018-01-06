using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.Models
{
    public class Room
    {
        public int IdRoom { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public bool? HasTV { get; set; }
        public bool? HasHairdryer { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public override string ToString()
        {
            return "idroom: " + IdRoom +
                " number : " + Number +
                " description : " + Description +
                " category : " + Type +
                " has wifi : " + Price +
                " has parking : " + HasTV +
                " phone: " + HasHairdryer +
                " hotel: " + Hotel.Name;
        }
    }
}