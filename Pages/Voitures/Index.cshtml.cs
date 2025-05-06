using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace ProjetReservation.Pages.Voitures
{
    public class IndexModel : PageModel
    {
        public List<Voiture>? Voitures { get; set; }
        public GestionVoiture GestionVoiture = new GestionVoiture(); 
        public void OnGet()
        {
            Voitures = GestionVoiture.ObtenirTout();
        }
    }
}
