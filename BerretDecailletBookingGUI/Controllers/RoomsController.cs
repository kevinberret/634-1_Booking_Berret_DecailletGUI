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
    public class RoomsController : Controller
    {
        private readonly string baseUri = "http://localhost:62837/api/Rooms/";

        // GET: Rooms
        public ActionResult Index()
        {
            List<Room> rooms;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri);
                rooms = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            return View("List", GetRoomSelectedVM(rooms));
        }

        [HttpPost]
        public ActionResult List(SearchVM svm)
        {
            List<Room> rooms;
            string request = "dateLoc/";
            request += svm.toString();

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri + request);
                rooms = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            return View(GetRoomSelectedVM(rooms, svm));
        }

        [HttpPost]
        public ActionResult AdvancedList(AdvancedSearchVM asvm)
        {
            List<Room> rooms;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri + asvm.ToString());
                rooms = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            return View("List", GetRoomSelectedVM(rooms, asvm));
        }

        private List<RoomSelectedVM> GetRoomSelectedVM(List<Room> rooms) {
            return GetRoomSelectedVM(rooms, null);
        }

        private List<RoomSelectedVM> GetRoomSelectedVM(List<Room> rooms, ISearchVM svm = null)
        {
            List<RoomSelectedVM> rsvm = new List<RoomSelectedVM>();

            foreach (Room r in rooms)
            {
                RoomSelectedVM tmp = new RoomSelectedVM();

                tmp.Room = r;

                if (svm != null)
                {
                    tmp.CheckIn = svm.CheckIn;
                    tmp.CheckOut = svm.CheckOut;
                }

                rsvm.Add(tmp);               
            }

            return rsvm;
        }
    }
}