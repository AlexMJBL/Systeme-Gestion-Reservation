using Gestion_Reservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;

using Reservations.Web.Services;
namespace Reservations.Web.Pages.ReservationsVoitures
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public ReservationVoiture ReservationVoiture { get; set; }
        GestionReservationVoiture gestionReservationVoiture = new GestionReservationVoiture();
        public ActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ReservationVoiture reservation = gestionReservationVoiture.ObtenirParId(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ReservationVoiture =reservation;
            return Page();
        }

        public IActionResult OnPost()
        {
            if(ReservationVoiture == null)
            {
                return NotFound();
            }

            var reservation = gestionReservationVoiture.ObtenirParId(ReservationVoiture.Id);
            if (reservation == null)
            {
                return NotFound();
            }


            ReservationVoiture = reservation;

            if (DateTime.Now >= ReservationVoiture.DateDebut)
            {
                ModelState.AddModelError("ReservationVoiture.DateDebut", "Vous ne pouvez pas annuler une réservation qui est commencée.");
                return Page();
            }

            gestionReservationVoiture.Supprimer(ReservationVoiture.Id);
            return RedirectToPage("./Index");
        }
    }
}
