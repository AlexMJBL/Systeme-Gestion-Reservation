using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using Reservations.Web.Services;

namespace Reservations.Web.Pages.ReservationsVoitures
{
    public class ConsulterModel : PageModel
    {
        public ReservationVoiture ReservationVoiture { get; set; }
        GestionReservationVoiture gestionReservationVoiture = new GestionReservationVoiture();

        public ActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReservationVoiture = gestionReservationVoiture.ObtenirParId(id);
            if (ReservationVoiture == null)
            {
                return NotFound();
            }
           
            return Page();

        }
    }
}
