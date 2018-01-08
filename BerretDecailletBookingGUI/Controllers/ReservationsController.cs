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

        // GET: Reservations
        public ActionResult Index()
        {
            List<Reservation> reservations;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(reservationUri);
                reservations = JsonConvert.DeserializeObject<List<Reservation>>(response.Result);
            }

            return View(reservations);
        }

        [HttpPost]
        public ActionResult Create(IEnumerable<RoomSelectedVM> rooms)
        {
            ReservationVM rvm = new ReservationVM();
            rvm.Rooms = new List<Room>();
            rvm.Reservation = new Reservation();

            rvm.Reservation.CheckIn = rooms.ElementAt(0).CheckIn;
            rvm.Reservation.CheckOut = rooms.ElementAt(0).CheckOut;

            foreach (RoomSelectedVM rsvm in rooms.Where(r => r.IsSelected == true))
            {
                rvm.Rooms.Add(new Room() { IdRoom = rsvm.Room.IdRoom });              
            }
            
            return View(rvm);
        }

        [HttpPost]
        public ActionResult Reserve(ReservationVM rvm) {
            // Get the rooms selected for the reservation
            rvm.Reservation.Rooms = new List<Room>();

            foreach(Room r in rvm.Rooms)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<String> response = httpClient.GetStringAsync(roomUri+r.IdRoom);
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
            return RedirectToAction("Details", new { id = reserv.IdReservation });
        }

        public ActionResult Details(int id)
        {
            Reservation reservation;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(reservationUri + id);
                reservation = JsonConvert.DeserializeObject<Reservation>(response.Result);
            }

            return View(reservation);
        }


        public ActionResult Delete(int id)
        {

            // Send the reservation delete to the system
            using (HttpClient httpClient = new HttpClient())
            {

                Task<HttpResponseMessage> response = httpClient.DeleteAsync(reservationUri + id);
            if (response.Result.IsSuccessStatusCode)
            {
                //
            }
        }

            return RedirectToAction("Index", "Home");
        }
    }
}