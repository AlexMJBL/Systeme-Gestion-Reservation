using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProjetReservation.Models
{

    public class ReservationVoiture : Reservation
    {
        [ValidateNever]
        public Voiture? Voiture { get; set; }
        [Display(Name = "Voiture")]
        [Required(ErrorMessage = "Veuillez sélectionner une voiture.")]
        public int VoitureId { get; set; }
        public double Prix { get { return Voiture != null ? Voiture.PrixJournalier * (Math.Abs((DateFin - DateDebut).Days)) : 0; } }
    }


}
