using Gestion_Reservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using Reservations.Web.Services;

namespace Gestion_Reservation.Pages.ReservationsChambresHotels
{
    public class ConsulterModel : PageModel
    {
        public ReservationChambreHotel ReservationChambreHotel { get; set; }
        GestionReservationChambreHotel gestionReservationChambreHotel = new GestionReservationChambreHotel();

        public ActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReservationChambreHotel = gestionReservationChambreHotel.ObtenirParId(id);
            if (ReservationChambreHotel == null)
            {
                return NotFound();
            }

            return Page();

        }
    }
}
