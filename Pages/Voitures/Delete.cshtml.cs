using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.Voitures
{
    public class DeleteModel : PageModel
    {
        [BindProperty]

        public Voiture? Voiture { get; set; }
        public GestionVoiture GestionVoiture = new GestionVoiture();

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Voiture = GestionVoiture.ObtenirParId(id);

            if(Voiture == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            GestionVoiture.Supprimer(Voiture.Id);
            return RedirectToPage("./Index");
        }
    }
}
