using BerretDecailletBookingGUI.Models;
using BerretDecailletBookingGUI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BerretDecailletBookingGUI.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly string reservationUri = "http://localhost:62837/api/Reservations/";
        private readonly string roomUri = "http://localhost:62837/api/Rooms/";

        /*
         * This method allows to create a reservation according to a list of selected rooms 
         * object, which contains checkin and checkout dates
         */
        [HttpPost]
        public ActionResult Create(IEnumerable<RoomSelectedVM> rooms)
        {
            // Create reservationvm object for reservation
            ReservationVM rvm = new ReservationVM();
            rvm.Rooms = new List<Room>();
            rvm.Reservation = new Reservation();

            rvm.Reservation.CheckIn = rooms.ElementAt(0).CheckIn;
            rvm.Reservation.CheckOut = rooms.ElementAt(0).CheckOut;

            // get only selected rooms
            foreach (RoomSelectedVM rsvm in rooms.Where(r => r.IsSelected == true))
            {
                rvm.Rooms.Add(new Room() { IdRoom = rsvm.Room.IdRoom });
            }

            return View(rvm);
        }

        /*
         * This method allows te make a reservation according to a reservationvm object,
         * which contains reservation object
         */
        [HttpPost]
        public ActionResult Reserve(ReservationVM rvm)
        {
            // Get the rooms selected for the reservation
            rvm.Reservation.Rooms = new List<Room>();

            foreach (Room r in rvm.Rooms)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<String> response = httpClient.GetStringAsync(roomUri + r.IdRoom);
                    rvm.Reservation.Rooms.Add(JsonConvert.DeserializeObject<Room>(response.Result));
                }
            }

            // Send the reservation to the system
            using (HttpClient httpClient = new HttpClient())
            {
                string pro = JsonConvert.SerializeObject(rvm.Reservation);
                StringContent frame = new StringContent(pro, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = httpClient.PostAsync(reservationUri, frame);
                if (response.Result.IsSuccessStatusCode)
                {
                    //
                }
            }

            /*
             * Get all reservations and get last one
             */
            List<Reservation> reservations;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(reservationUri);
                reservations = JsonConvert.DeserializeObject<List<Reservation>>(response.Result);
            }
            Reservation reserv = reservations.ElementAt(0);
            foreach (Reservation res in reservations)
            {
                if (res.IdReservation > reserv.IdReservation)
                    reserv = res;
            }

            // Redirect to reservation details
            return RedirectToAction("Details", new { id = reserv.IdReservation });
        }

        /*
         * This method allows to display a reservation and its details
         */
        public ActionResult Details(int id)
        {
            // Get reservation
            Reservation reservation;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(reservationUri + id);
                reservation = JsonConvert.DeserializeObject<Reservation>(response.Result);
            }

            return View(reservation);
        }

        /*
         * This method allows the user to delete a specific reservation with its informations
         */
        public ActionResult Delete()
        {
            return View(new Reservation());
        }

        /*
         * This method get a reservation object and check if it matches one in the database
         * If it does => send delete command
         * If it does not => send message error
         */
        [HttpPost]
        public ActionResult Delete(Reservation reservation)
        {
            Reservation tmp;

            // Get reservation to see if it matches
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(reservationUri + reservation.IdReservation);
                try
                {
                    tmp = JsonConvert.DeserializeObject<Reservation>(response.Result);
                }
                catch (Exception e)
                {
                    tmp = null;
                    TempData["Error"] = "Incorrect reservation number";
                }

            }

            // If data set by user match data on server, send the reservation delete to the system
            if (tmp != null && tmp.Fisrtname.Equals(reservation.Fisrtname) && tmp.Lastname.Equals(reservation.Lastname))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<HttpResponseMessage> response = httpClient.DeleteAsync(reservationUri + reservation.IdReservation);
                    TempData["Success"] = "Reservation deleted";
                }
            }
            else if (TempData["Error"] == null)
            {
                TempData["Error"] = "Incorrect reservation informations";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}