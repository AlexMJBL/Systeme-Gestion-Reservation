using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.ChambresHotels
{
    public class CreateModel : PageModel
    {
        [BindProperty]

        public ChambreHotel ChambreHotel { get; set; } = new ChambreHotel();
        public GestionChambreHotel GestionChambreHotel = new GestionChambreHotel();

        public void OnGet()
        {
            ChambreHotel.Id = GestionChambreHotel.ObtenirProchainId();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GestionChambreHotel.Ajouter(ChambreHotel);
            return RedirectToPage("./Index");
        }
    }
}
