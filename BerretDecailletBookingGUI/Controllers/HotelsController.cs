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
    public class HotelsController : Controller
    {
        private readonly string baseUri = "http://localhost:62837/api/Hotels/";

        /*
         * This method allows to display details of an hotel
         */
        public ActionResult Details(int id) {
            Hotel hotel;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri + id);
                hotel = JsonConvert.DeserializeObject<Hotel>(response.Result);
            }

            return View(hotel);
        }
    }
}