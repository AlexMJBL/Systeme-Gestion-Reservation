using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using Reservations.Web.Services;

namespace Reservations.Web.Pages.ReservationsVoitures
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ReservationVoiture? ReservationVoiture { get; set; }
        GestionReservationVoiture gestionReservationVoiture = new GestionReservationVoiture();
        public List<Voiture> Voitures { get; private set; } = new List<Voiture>();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Voitures = gestionReservationVoiture.Voitures;
            
            var reservation = gestionReservationVoiture.ObtenirParId(id);

            if (reservation == null)
            {
                return NotFound();
            }

            ReservationVoiture = reservation;
            return Page();
        }
        public IActionResult OnPost()
        {
            Voitures = gestionReservationVoiture.Voitures;
            ReservationVoiture.Voiture = Voitures.SingleOrDefault(v => v.Id == ReservationVoiture.VoitureId);

            //Validation Date
            if (ReservationVoiture.DateDebut < DateTime.Now)
            {
                ModelState.AddModelError("ReservationVoiture.DateDebut", "Veuillez sélectionner une date de début plus récente que la date actuel.");
                return Page();
            }

            if (ReservationVoiture.DateFin <= ReservationVoiture.DateDebut)
            {
                ModelState.AddModelError("ReservationVoiture.DateFin", "Veuillez sélectionner une date de fin plus récente que la date de début.");
                return Page();
            }

            //Validation Voiture
            if (ReservationVoiture.Voiture == null)
            {
                ModelState.AddModelError("ReservationVoiture.Voiture", "Veuillez sélectionner une voiture.");
                return Page();
            }

            //Validation Disponibilité
            bool nonDisponible = gestionReservationVoiture.EstReserver(ReservationVoiture);
            if (nonDisponible)
            {
                ModelState.AddModelError("ReservationVoiture.Voiture", "La voiture n'est pas disponible pour les dates sélectionnées.");
                return Page();
            }

            if (DateTime.Now >= ReservationVoiture.DateDebut)
            {
                ModelState.AddModelError("ReservationVoiture.DateDebut", "Vous ne pouvez pas modifier une réservation qui est commencée.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            gestionReservationVoiture.Modifier(ReservationVoiture);
            return RedirectToPage("./Index");
        }
    }
}
