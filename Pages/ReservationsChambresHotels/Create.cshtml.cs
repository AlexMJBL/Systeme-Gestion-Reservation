using Gestion_Reservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using Reservations.Web.Services;

namespace Gestion_Reservation.Pages.ReservationsChambresHotels
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ReservationChambreHotel ReservationChambreHotel { get; set; } = new ReservationChambreHotel();
        GestionReservationChambreHotel gestionReservationChambreHotel = new GestionReservationChambreHotel();
        public List<ChambreHotel> ChambresHotel { get; private set; } = new List<ChambreHotel>();

        public void OnGet()
        {
            ChambresHotel = gestionReservationChambreHotel.Reservables;
            ReservationChambreHotel.Id = gestionReservationChambreHotel.GenererId();
            ReservationChambreHotel.DateDebut = DateTime.Now.AddDays(1);
            ReservationChambreHotel.DateFin = DateTime.Now.AddDays(2);
        }
        public IActionResult OnPost()
        {
            ChambresHotel = gestionReservationChambreHotel.Reservables;

            //Validation Date
            if (ReservationChambreHotel.DateDebut < DateTime.Now)
            {
                ModelState.AddModelError("ReservationChambreHotel.DateDebut", "Veuillez s�lectionner une date de d�but plus r�cente que la date actuel.");
                return Page();
            }
            if (ReservationChambreHotel.DateFin <= ReservationChambreHotel.DateDebut)
            {
                ModelState.AddModelError("ReservationChambreHotel.DateFin", "Veuillez s�lectionner une date de fin plus r�cente que la date de d�but.");
                return Page();
            }


            //Validation Chambre

            var chambreId = ReservationChambreHotel.ChambreHotel?.Id ?? 0;
            ReservationChambreHotel.ChambreHotel = ChambresHotel.SingleOrDefault(v => v.Id == ReservationChambreHotel.ChambreId);

            if (ReservationChambreHotel.ChambreHotel == null)
            {
                ModelState.AddModelError("ReservationChambreHotel.ChambreId", "Veuillez s�lectionner une chambre.");
                return Page();
            }

            //Validation Disponibilit�
            bool nonDisponible = gestionReservationChambreHotel.EstReserver(ReservationChambreHotel);
            if (nonDisponible)
            {
                ModelState.AddModelError("ReservationChambreHotel.ChambreId", "La chambre n'est pas disponible pour les dates s�lectionn�es.");
                return Page();
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }

            gestionReservationChambreHotel.Ajouter(ReservationChambreHotel);
            return RedirectToPage("./Index");
        }
    }
}
