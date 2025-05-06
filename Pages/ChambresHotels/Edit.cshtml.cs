using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.ChambresHotels
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ChambreHotel? ChambreHotel { get; set; }
        public GestionChambreHotel GestionChambreHotel = new GestionChambreHotel();

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            ChambreHotel = GestionChambreHotel.ObtenirParId(id);

            if (ChambreHotel == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GestionChambreHotel.Modifier(ChambreHotel);

            return RedirectToPage("./Index");
        }
    }
}
