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

        /**
         * This method allows to list rooms according to a search object passed in parameter
         * If checkin and checkout dates are null, it displays an error
         */
        [HttpPost]
        public ActionResult List(SearchVM svm)
        {
            // Verify checkin and checkout
            DateTime emptyDate = new DateTime(0001, 01, 01, 00, 00, 00);

            if(svm.CheckIn == emptyDate || svm.CheckOut == emptyDate)
            {
                TempData["Error"] = "Please verify your checkin and checkout dates.";
                return RedirectToAction("Index", "Home");
            }

            // Get rooms and display them
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

        /**
         * This method allows to list rooms according to an advanced search object passed in parameter
         * If checkin and checkout dates are null, it displays an error
         */
        [HttpPost]
        public ActionResult AdvancedList(AdvancedSearchVM asvm)
        {
            // Verify checkin and checkout
            DateTime emptyDate = new DateTime(0001, 01, 01, 00, 00, 00);

            if (asvm.CheckIn == emptyDate || asvm.CheckOut == emptyDate)
            {
                TempData["Error"] = "Please check your checkin and checkout dates.";
                return RedirectToAction("Index", "Home");
            }

            // Get rooms and display them
            List<Room> rooms;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(baseUri + asvm.ToString());
                rooms = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            if (rooms.Count == 0)
            {
                TempData["Error"] = "No rooms found matching your wishes";
            }

            return View("List", GetRoomSelectedVM(rooms, asvm));
        }

        /**
         * This method creates a RoomSelected object according to a list of rooms and a search object so they
         * can be displayed in a list
         */
        private List<RoomSelectedVM> GetRoomSelectedVM(List<Room> rooms, ISearchVM svm)
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