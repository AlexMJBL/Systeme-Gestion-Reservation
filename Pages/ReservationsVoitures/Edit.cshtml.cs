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

            Voitures = gestionReservationVoiture.Reservables;
            
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
            Voitures = gestionReservationVoiture.Reservables;
            ReservationVoiture.Voiture = Voitures.SingleOrDefault(v => v.Id == ReservationVoiture.VoitureId);

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

            if (DateTime.Now >= ReservationVoiture.DateDebut)
            {
                ModelState.AddModelError("ReservationVoiture.DateDebut", "Vous ne pouvez pas modifier une r�servation qui est commenc�e.");
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
