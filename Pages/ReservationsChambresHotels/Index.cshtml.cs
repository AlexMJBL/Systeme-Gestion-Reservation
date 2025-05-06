using Gestion_Reservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;


namespace Gestion_Reservation.Pages.ReservationsChambresHotels
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string OptionChoisi { get; set; } = "";
        GestionReservationChambreHotel gestionReservationChambreHotel = new GestionReservationChambreHotel();
        public List<ReservationChambreHotel> ReservationsChambresHotels { get; private set; } = new List<ReservationChambreHotel>();

        public void OnGet()
        {
            ReservationsChambresHotels = gestionReservationChambreHotel.ObtenirTout()
            .OrderBy(r => r.DateDebut).ToList();
        }

        public void OnPost()
        {

            ReservationsChambresHotels = gestionReservationChambreHotel.ObtenirTout();
            ReservationsChambresHotels = OptionChoisi switch
            {
                "Prix" => ReservationsChambresHotels.OrderByDescending(r => r.Prix).ToList(),
                "DateDebut" => ReservationsChambresHotels.OrderByDescending(r => r.DateDebut).ToList(),
                "DateFin" => ReservationsChambresHotels.OrderByDescending(r => r.DateFin).ToList(),
                _ => ReservationsChambresHotels
            };
        }

    }
}
