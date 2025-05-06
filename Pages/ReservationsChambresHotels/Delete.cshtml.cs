using Gestion_Reservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using Reservations.Web.Services;

namespace Gestion_Reservation.Pages.ReservationsChambresHotels
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
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

        public IActionResult OnPost()
        {
            if (ReservationChambreHotel == null)
            {
                return NotFound();
            }


            var reservation = gestionReservationChambreHotel.ObtenirParId(ReservationChambreHotel.Id);
            if (reservation == null)
            {
                return NotFound();
            }

            
            ReservationChambreHotel = reservation;

            if (DateTime.Now >= ReservationChambreHotel.DateDebut)
            {
                ModelState.AddModelError("ReservationChambreHotel.DateDebut", "Vous ne pouvez pas annuler une réservation qui est commencée.");
                return Page();
            }

            gestionReservationChambreHotel.Supprimer(ReservationChambreHotel.Id);
            return RedirectToPage("./Index");
        }
    }
}
