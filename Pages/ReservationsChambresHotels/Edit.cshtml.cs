using Gestion_Reservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;

namespace Gestion_Reservation.Pages.ReservationsChambresHotels
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ReservationChambreHotel ReservationChambreHotel { get; set; } = new ReservationChambreHotel();
        GestionReservationChambreHotel gestionReservationChambreHotel = new GestionReservationChambreHotel();
        public List<ChambreHotel> ChambresHotel { get; private set; } = new List<ChambreHotel>();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           ChambresHotel = gestionReservationChambreHotel.Reservables;

            var reservation = gestionReservationChambreHotel.ObtenirParId(id);

            if (reservation == null)
            {
                return NotFound();
            }

            ReservationChambreHotel = reservation;
            return Page();
        }
        public IActionResult OnPost()
        {
            ChambresHotel = gestionReservationChambreHotel.Reservables;
            ReservationChambreHotel.ChambreHotel = ChambresHotel.SingleOrDefault(v => v.Id == ReservationChambreHotel.ChambreId);

            //Validation Date
            if (ReservationChambreHotel.DateDebut < DateTime.Now)
            {
                ModelState.AddModelError("ReservationChambreHotel.DateDebut", "Veuillez sélectionner une date de début plus récente que la date actuel.");
                return Page();
            }
            if (ReservationChambreHotel.DateFin <= ReservationChambreHotel.DateDebut)
            {
                ModelState.AddModelError("ReservationChambreHotel.DateFin", "Veuillez sélectionner une date de fin plus récente que la date de début.");
                return Page();
            }


            //Validation Chambre
            
            if (ReservationChambreHotel.ChambreHotel == null)
            {
                ModelState.AddModelError("ReservationChambreHotel.ChambreId", "Veuillez sélectionner une chambre.");
                return Page();
            }

            //Validation Disponibilité
            bool nonDisponible = gestionReservationChambreHotel.EstReserver(ReservationChambreHotel);
            if (nonDisponible)
            {
                ModelState.AddModelError("ReservationChambreHotel.ChambreId", "La chambre n'est pas disponible pour les dates sélectionnées.");
                return Page();
            }

            if (DateTime.Now >= ReservationChambreHotel.DateDebut)
            {
                ModelState.AddModelError("ReservationChambreHotel.DateDebut", "Vous ne pouvez pas modifier une réservation qui est commencée.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            gestionReservationChambreHotel.Modifier(ReservationChambreHotel);
            return RedirectToPage("./Index");
        }
    }
}
