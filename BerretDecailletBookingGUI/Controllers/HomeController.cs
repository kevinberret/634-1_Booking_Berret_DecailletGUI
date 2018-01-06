using BerretDecailletBookingGUI.Models;
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
        private readonly string baseUri = "http://localhost:62837/api/Hotels";

        // GET: Home
        public ActionResult Index()
        {
            List<Hotel> hotels;
            List<String> locations = new List<string>();

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri);
                hotels = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }

            foreach (Hotel h in hotels)
            {
                if (!locations.Contains(h.Location))
                {
                    locations.Add(h.Location);
                }
            }

            return View(locations);
        }
    }
}