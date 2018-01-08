using BerretDecailletBookingGUI.Models;
using BerretDecailletBookingGUI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BerretDecailletBookingGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly string hotelUri = "http://localhost:62837/api/Hotels/";

        // Home page with basic search function
        public ActionResult Index()
        {
            SearchVM svm = new SearchVM();
            svm.Locations = getLocations();

            return View(svm);
        }

        // Advanced search
        public ActionResult Search()
        {
            AdvancedSearchVM asvm = new AdvancedSearchVM();
            asvm.Locations = getLocations();

            return View(asvm);
        }

        /*
         * This method get all locations available in database
         */ 
        private List<String> getLocations()
        {
            List<Hotel> hotels;
            List<String> locations = new List<string>();

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(hotelUri);
                hotels = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }

            foreach (Hotel h in hotels)
            {
                if (!locations.Contains(h.Location))
                {
                    locations.Add(h.Location);
                }
            }

            return locations;
        }
    }
}