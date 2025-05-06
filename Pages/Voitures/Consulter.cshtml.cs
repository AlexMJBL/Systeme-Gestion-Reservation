using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.Voitures
{
    public class ConsulterModel : PageModel
    {
        public Voiture? Voiture { get; set; }
        public GestionVoiture GestionVoiture = new GestionVoiture();

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Voiture? voiture = GestionVoiture.ObtenirParId(id);

            if (voiture == null)
            {
                return NotFound();
            }

            Voiture = voiture;
            return Page();
        }
    }
}
