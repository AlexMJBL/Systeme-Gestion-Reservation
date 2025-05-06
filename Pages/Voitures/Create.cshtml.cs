using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.Voitures
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Voiture Voiture { get; set; } = new Voiture();
        public GestionVoiture GestionVoiture = new GestionVoiture();

        public void OnGet()
        {
            Voiture.Id = GestionVoiture.ObtenirProchainId();
        }

        public IActionResult OnPost()
        {
            int anneeLimite = DateTime.Now.Year - 10;

            //Verifie seulement si l'annee de fabrication est plus grande que l'annee limite, nous ne verifions pas que la voiture est moin recente que l'annee actuel car les model 2026 sont disponible
            // avant la fin de l'annee 2025.
            if (Voiture.AnneeDeFabrication < anneeLimite)
            {
                ModelState.AddModelError("Voiture.AnneeDeFabrication", "La voiture doit avoir moins de 10 ans");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            GestionVoiture.Ajouter(Voiture);
            return RedirectToPage("./Index");
        }
    }
}
