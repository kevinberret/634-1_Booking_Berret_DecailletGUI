using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BerretDecailletBookingGUI.ViewModel
{
    public class AdvancedSearchVM:ISearchVM
    {
        public string Name { get; set; }        
        public String Location { get; set; }
        public List<String> Locations { get; set; }
        public int RoomType { get; set; }
        public bool HasWifi { get; set; }
        public bool HasHairdryer { get; set; }
        public bool HasParking { get; set; }
        public bool HasTV { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }

        public AdvancedSearchVM()
        {
            PriceMax = 3000;
            PriceMin = 0;
            CheckIn = DateTime.Today;
            CheckOut = CheckIn.Add(new TimeSpan(1, 0, 0, 0));
        }

        public String ToString() {
            string parameters = "advanced?";

            parameters += "checkin=" + CheckIn.ToString("yyyy-MM-dd");
            parameters += "&checkout=" + CheckOut.ToString("yyyy-MM-dd");
            parameters += "&type=" + RoomType;
            parameters += "&minprice=" + PriceMin;
            parameters += "&maxprice=" + PriceMax;

            if (Name != null)
                parameters += "&name=" + Name;

            if(Location != null)
                parameters += "&location=" + Location;

            if(HasWifi != false)
                parameters += "&wifi=" + true;

            if (HasHairdryer != false)
                parameters += "&hairdryer=" + HasHairdryer;

            if (HasParking != false)
                parameters += "&parking=" + HasParking;

            if (HasTV != false)
                parameters += "&tv=" + HasTV;

            return parameters;
        }
    }
}