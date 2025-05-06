using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reservations.Web.Services;
using ProjetReservation.Models;

namespace Reservations.Web.Pages.ReservationsVoitures
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string OptionChoisi { get; set; } = "";
        GestionReservationVoiture gestionReservationVoiture = new GestionReservationVoiture();
        public List<ReservationVoiture> ReservationsVoitures { get; private set; } = new List<ReservationVoiture>();

        public void OnGet()
        {
            ReservationsVoitures = gestionReservationVoiture.ObtenirTout()
            .OrderBy(r => r.DateDebut).ToList();
        }

        public void OnPost() {
            
            ReservationsVoitures = gestionReservationVoiture.ObtenirTout();
            ReservationsVoitures = OptionChoisi switch
            {
                "Prix" => ReservationsVoitures.OrderByDescending(r => r.Prix).ToList(),
                "DateDebut" => ReservationsVoitures.OrderByDescending(r => r.DateDebut).ToList(),
                "DateFin" => ReservationsVoitures.OrderByDescending(r => r.DateFin).ToList(),
                _ => ReservationsVoitures
            };
        }

    }
}
