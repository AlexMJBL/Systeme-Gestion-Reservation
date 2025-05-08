using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Reservations.Web.Services;

using ProjetReservation.Models;

namespace Reservations.Web.Pages.ReservationsVoitures
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ReservationVoiture ReservationVoiture { get; set; } = new ReservationVoiture();
        GestionReservationVoiture gestionReservationVoiture = new GestionReservationVoiture();
        public List<Voiture> Voitures { get; private set; } = new List<Voiture>();

        public void OnGet()
        {
            Voitures = gestionReservationVoiture.Reservables;
            ReservationVoiture.Id = gestionReservationVoiture.GenererId();
            ReservationVoiture.DateDebut = DateTime.Now.AddDays(1);
            ReservationVoiture.DateFin = DateTime.Now.AddDays(2);
        }
        public IActionResult OnPost()
        {
            Voitures = gestionReservationVoiture.Reservables;

            //Validation Date
            if (ReservationVoiture.DateDebut < DateTime.Now)
            {
                ModelState.AddModelError("ReservationVoiture.DateDebut", "Veuillez s�lectionner une date de d�but plus r�cente que la date actuel.");
                return Page();
            }
            if (ReservationVoiture.DateFin <= ReservationVoiture.DateDebut)
            {
                ModelState.AddModelError("ReservationVoiture.DateFin", "Veuillez s�lectionner une date de fin plus r�cente que la date de d�but.");
                return Page();
            }


            //Validation Voiture

            var voitureId = ReservationVoiture.Voiture?.Id ?? 0;
            ReservationVoiture.Voiture = Voitures.SingleOrDefault(v => v.Id == ReservationVoiture.VoitureId);

            if (ReservationVoiture.Voiture == null)
            {
                ModelState.AddModelError("ReservationVoiture.VoitureId", "Veuillez s�lectionner une voiture.");
                return Page();
            }

            //Validation Disponibilit�
            bool nonDisponible = gestionReservationVoiture.EstReserver(ReservationVoiture);
            if (nonDisponible)
            {
                ModelState.AddModelError("ReservationVoiture.VoitureId", "La voiture n'est pas disponible pour les dates s�lectionn�es.");
                return Page();
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }

            gestionReservationVoiture.Ajouter(ReservationVoiture);
            return RedirectToPage("./Index");
        }
    }
}

