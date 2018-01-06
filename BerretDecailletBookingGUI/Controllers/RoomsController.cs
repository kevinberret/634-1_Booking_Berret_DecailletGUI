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
    public class RoomsController : Controller
    {
        private readonly string baseUri = "http://localhost:62837/api/Rooms";

        // GET: Rooms
        public ActionResult Index()
        {
            List<Room> rooms;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri);
                rooms = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            return View("List", rooms);
        }
    }
}