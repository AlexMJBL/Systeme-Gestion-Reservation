using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetReservation.Models;
using ProjetReservation.Services;



namespace ProjetReservation.Pages.ChambresHotels
{
    public class IndexModel : PageModel
    {
        public string OptionChoisi { get; set; } = "";
        public List<ChambreHotel>? ChambreHotel { get; set; }
        public GestionChambreHotel GestionChambreHotel = new GestionChambreHotel();

        public void OnGet()
        {
            ChambreHotel = GestionChambreHotel.ObtenirTout();
        }

       
    }
}
