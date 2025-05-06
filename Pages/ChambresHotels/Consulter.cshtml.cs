using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.ChambresHotels
{
    public class ConsulterModel : PageModel
    {
        public ChambreHotel? ChambreHotel { get; set; }
        public GestionChambreHotel GestionChambreHotel = new GestionChambreHotel();

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            ChambreHotel? chambreHotel = GestionChambreHotel.ObtenirParId(id);

            if (chambreHotel == null)
            {
                return NotFound();
            }

            ChambreHotel = chambreHotel;
            return Page();
        }
    }
}
